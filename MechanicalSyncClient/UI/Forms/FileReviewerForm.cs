using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Properties;
using MechanicalSyncApp.Reviews.FileReviewer;
using Serilog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class FileReviewerForm : Form
    {
        private readonly IFileReviewer fileReviewer;
        private readonly IMechSyncServiceClient syncServiceClient;
        private readonly LocalReview review;
        private readonly ILogger logger;

        private readonly string tempWorkingCopyDirectory;

        private ISolidWorksStarter solidWorksStarter;

        public FileReviewerForm(
                IAuthenticationServiceClient authServiceClient,
                IMechSyncServiceClient syncServiceClient,
                LocalReview review,
                ILogger logger
            )
        {
            InitializeComponent();
            this.syncServiceClient = syncServiceClient ?? throw new ArgumentNullException(nameof(syncServiceClient));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            tempWorkingCopyDirectory = Path.Combine(Path.GetTempPath(), $"review_{review.RemoteVersion.Id}");
            solidWorksStarter = new SolidWorksStarter(logger)
            {
                Hidden = false,
                SolidWorksExePath = Settings.Default.SOLIDWORKS_EXE_PATH,
                ShowSplash = false,
                SolidWorksStartTimeoutSeconds = 60
            };
            (solidWorksStarter as SolidWorksStarter).OnDestroy += FileReviewerForm_OnSolidWorksDestroy;

            DeltaFilesTreeView.ImageList = TreeViewIcons;

            if (review.RemoteReview.TargetType == "AssemblyFile")
                Text = "3D Review";
            else if (review.RemoteReview.TargetType == "DrawingFile")
                Text = "2D Review";
            else
                throw new Exception($"Unsupported TargetType in review: {review.RemoteReview.TargetType}");

            var ui = new FileReviewerUI()
            {
                MainSplit = MainSplit,
                DeltaFilesTreeView = DeltaFilesTreeView,
                HeaderLabel = HeaderLabel,
                DesignerLabel = DesignerLabel,
                CloseFileReviewButton = CloseFileButton,
                ReviewToolStrip = ReviewToolStrip,
                StatusLabel = StatusLabel,
                ReviewTargetStatus = ReviewTargetStatus,
                ChangeRequestInput = ChangeRequestInput,
                ChangeRequestsGrid = ChangeRequestsGrid,
                ApproveFileButton = ApproveFileButton,
                RejectFileButton = RejectFileButton,
                RefreshReviewTargetsButton = RefreshReviewTargetsButton,
                ChangeRequestSplit = ChangeRequestSplit,
            };
            fileReviewer = new FileReviewer(
                new FileReviewerArgs()
                {
                    AuthServiceClient = authServiceClient,
                    SyncServiceClient = syncServiceClient,
                    UI = ui,
                    Review = review,
                    Logger = logger,
                    TempWorkingCopyDirectory = tempWorkingCopyDirectory,
                    SolidWorksStarter = solidWorksStarter
                }
            );
        }

        private void FileReviewerForm_OnSolidWorksDestroy(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(Close));
            else
                Close();
        }


        private async void FileReviewerForm_Load(object sender, EventArgs e)
        {
            try
            {
                var ui = fileReviewer.Args.UI;
                var sw = fileReviewer.Args.SolidWorksStarter;

                ui.DeltaFilesTreeView.Enabled = false;
                await fileReviewer.InitializeUiAsync();
                await sw.StartSolidWorksAsync();
                await DownloadTemporaryWorkingCopyAsync();
                ui.DeltaFilesTreeView.Enabled = true;
            }
            catch (Exception ex)
            {
                var message = $"Failed to open review: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void FileReviewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                CleanupTempWorkingCopy();
                solidWorksStarter?.Dispose();
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            finally
            {
                // go back to the main form
                VersionSynchronizerForm.Instance.Show();
            }
        }

        private void CleanupTempWorkingCopy()
        {
            if (!Directory.Exists(tempWorkingCopyDirectory))
                return;
            
            // Remove read-only attribute from all files within the directory
            var files = Directory.GetFiles(tempWorkingCopyDirectory, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileAttributes attributes = File.GetAttributes(file);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(file, attributes & ~FileAttributes.ReadOnly);
            }
            Directory.Delete(tempWorkingCopyDirectory, true);
        }

        private async Task DownloadTemporaryWorkingCopyAsync()
        {
            CleanupTempWorkingCopy();

            // create empty temporary working copy directory to put downloaded files
            Directory.CreateDirectory(tempWorkingCopyDirectory);

            using (var cts = new CancellationTokenSource())
            {
                // progress dialog will inform user about download progress
                var progressDialog = new DownloadWorkingCopyDialog(cts);

                try
                {
                    progressDialog.SetTitle("Download copy for review");
                    progressDialog.SetOpeningLegend($"Downloading version files...");
                    progressDialog.Show();

                    var allFileMetadata = await syncServiceClient.GetFileMetadataAsync(review.RemoteVersion.Id, null);
                    var totalFiles = allFileMetadata.Count;

                    int i = 0;

                    // download each file based on its metadata
                    foreach (var fileMetadata in allFileMetadata)
                    {
                        cts.Token.ThrowIfCancellationRequested();

                        var localFilePath = Path.Combine(tempWorkingCopyDirectory, fileMetadata.RelativeFilePath);

                        // remote file path is linux-based with forward slash, convert to windows-based
                        localFilePath = localFilePath.Replace('/', Path.DirectorySeparatorChar);

                        progressDialog.SetStatus($"Downloading {Path.GetFileName(localFilePath)}...");

                        var fileDirectory = Path.GetDirectoryName(localFilePath);
                        if (!Directory.Exists(fileDirectory))
                            Directory.CreateDirectory(fileDirectory);

                        await syncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                        {
                            LocalFilename = localFilePath,
                            RelativeEquipmentPath = review.RemoteProject.RelativeEquipmentPath,
                            RelativeFilePath = fileMetadata.RelativeFilePath,
                            VersionFolder = "Ongoing"
                        });

                        // put downloaded file as read-only
                        if (File.Exists(localFilePath))
                            File.SetAttributes(localFilePath, File.GetAttributes(localFilePath) | FileAttributes.ReadOnly);

                        i++;
                        var currentProgress = totalFiles > 0
                            ? (int)((double)i / totalFiles * 100.0)
                            : 0;

                        progressDialog.SetProgress(currentProgress);
                    }
                }
                finally
                {
                    progressDialog.Close();
                }
            }
        }
    }
}
