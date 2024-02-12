using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Explorer
{
    public class ProjectExplorer
    {
        private readonly IAuthenticationServiceClient authServiceClient;
        private readonly IMechSyncServiceClient mechSyncService;
        private readonly IProjectExplorerUI UI;

        public AggregatedProjectDetails CurrentProject { get; private set; }

 
        private readonly ConcurrentDictionary<string, UserDetails> userDetailsIndex = new ConcurrentDictionary<string, UserDetails>();

        public ProjectExplorer(IAuthenticationServiceClient authServiceClient,
                               IMechSyncServiceClient mechSyncServiceClient,
                               IProjectExplorerUI ui)
        {
            this.authServiceClient = authServiceClient ?? throw new ArgumentNullException(nameof(authServiceClient));
            this.mechSyncService = mechSyncServiceClient ?? throw new ArgumentNullException(nameof(mechSyncServiceClient));
            UI = ui ?? throw new ArgumentNullException(nameof(ui));


            UI.ProjectList.MouseDoubleClick += ProjectList_MouseDoubleClick;
            UI.RefreshFilesButton.Click += RefreshFilesButton_Click;
            UI.CloseProjectButton.Click += CloseProjectButton_Click;
            UI.VersionSelector.SelectedIndexChanged += VersionSelector_SelectedIndexChanged;
            UI.ProjectSearchFilter.KeyDown += ProjectSearchFilter_KeyDown;
            UI.FileSearchFilter.KeyDown += FileSearchFilter_KeyDown;

            UI.FileList.SetDoubleBuffered();
            UI.FileList.MouseDoubleClick += FileList_MouseDoubleClick;

            UI.ShowProjectList();
        }

     
        public async Task RefreshProjectListAsync(string searchFilter = null)
        {
            UI.ProjectList.Items.Clear();

            await Task.Run(async () =>
            {
                var projects = await mechSyncService.AggregateProjectDetailsAsync();

                if (searchFilter != null)
                {
                    projects = projects
                        .Where(pd =>
                            pd.ProjectDetails.FolderName.ToLower().Contains(searchFilter.ToLower()) ||
                            pd.ProjectDetails.CreatedAt.ToString().ToLower().Contains(searchFilter.ToLower())
                        ).ToList();
                }
                projects = projects.OrderByDescending(pd => pd.ProjectDetails.CreatedAt).ToList();

                UI.ProjectList.Invoke(new Action(async () =>
                {
                    foreach (var projectDetails in projects)
                    {
                        await AddProjectToListAsync(projectDetails);
                    }
                }));
            });
        }

        public async Task RefreshVersionFilesAsync()
        {
            UI.VersionFilesStatusLabel.Text = "Loading files...";

            UI.RefreshFilesButton.Enabled = false;
            UI.FileSearchFilter.Enabled = false;
            UI.VersionSelector.Enabled = false;
            UI.FileList.Items.Clear();

            if (UI.VersionSelector.Items.Count == 0 || UI.VersionSelector.SelectedIndex < 0)
                return;

            Version version = null;

            if (UI.VersionSelector.Text == "Latest")
                version = CurrentProject.LatestVersion;
            else if (UI.VersionSelector.Text == "Ongoing")
                version = CurrentProject.OngoingVersion;

            if (version == null) return;

            var filter = UI.FileSearchFilter.Text;

            var fileCount = await Task.Run(async () => {
                var files = await mechSyncService.GetFileMetadataAsync(version.Id, null);

                if (filter != "")
                    files = files.Where((f) => f.RelativeFilePath.ToLower().Contains(filter.ToLower())).ToList();

                files = files.OrderBy((f) => f.RelativeFilePath).ToList();

                UI.FileList.Invoke(new Action(() => {
                    foreach (var file in files)
                    {
                        long fileSizeInBytes = file.FileSize;
                        double fileSizeInMB = fileSizeInBytes / (1024.0 * 1024.0);
                        string formattedFileSize = $"{fileSizeInMB:F2} MB";

                        var item = new ListViewItem(file.RelativeFilePath);
                        item.SubItems.Add(formattedFileSize);
                        item.SubItems.Add(file.UploadedAt.ToString());
                        item.SubItems.Add(file.FileChecksum);
                        item.Tag = file;
                        item.ImageIndex = 0;
                        var groupIndex = GetFileGroupIndex(file.RelativeFilePath);
                        item.Group = UI.FileList.Groups[groupIndex];
                        UI.FileList.Items.Add(item);
                    }
                }));
                return files.Count;
            });

            UI.RefreshFilesButton.Enabled = true;
            UI.FileSearchFilter.Enabled = true;
            UI.VersionSelector.Enabled = true;

            UI.VersionFilesStatusLabel.Text = $"Showing {fileCount} files.";
        }

        public void OpenProject(AggregatedProjectDetails projectDetails)
        {
            CurrentProject = projectDetails;
            UI.ProjectFolderNameLabel.Text = CurrentProject.ToString();

            UI.VersionSelector.Items.Clear();

            if (CurrentProject.LatestVersion != null)
                UI.VersionSelector.Items.Add("Latest");

            if (CurrentProject.OngoingVersion != null)
                UI.VersionSelector.Items.Add("Ongoing");

            if (UI.VersionSelector.Items.Count > 0)
                UI.VersionSelector.SelectedIndex = 0;

            UI.ShowProjectFiles();
        }

        public void CloseProject()
        {
            CurrentProject = null;
            UI.ShowProjectList();
        }

        public async Task ShowFileInViewerAsync(FileMetadata fileMetadata, string versionFolder)
        {
            string relativeEquipmentPath = CurrentProject.ProjectDetails.RelativeEquipmentPath;
            Guid explorerTransactionId = Guid.NewGuid();

            Log.Debug($"Preparing to show file in viewer, explorerTransactionId = {explorerTransactionId}");

            if (fileMetadata is null)
                throw new ArgumentNullException(nameof(fileMetadata));

            if (versionFolder is null)
                throw new ArgumentNullException(nameof(versionFolder));

            var tempFile = Path.GetTempFileName().Replace(".tmp", Path.GetExtension(fileMetadata.RelativeFilePath));
            Log.Debug($"Created temporary file name: {tempFile}");
            try
            {
                UI.FileList.Enabled = false;

                Log.Debug("Downloading file from server to temporary file path...");
                UI.VersionFilesStatusLabel.Text = "Downloading file from server...";
                await mechSyncService.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = tempFile,
                    RelativeEquipmentPath = relativeEquipmentPath,
                    RelativeFilePath = fileMetadata.RelativeFilePath,
                    VersionFolder = versionFolder,
                    ExplorerTransactionId = explorerTransactionId.ToString(),
                });

                Log.Debug("Creating viewer instance...");
                var fileName = Path.GetFileName(fileMetadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                var viewer = new DesignFileViewerForm(tempFile, fileName, versionFolder);

                Log.Debug("Initializing viewer...");
                viewer.Initialize();

                UI.VersionFilesStatusLabel.Text = "Showing file in viewer...";
                Log.Debug("Showing file in viewer dialog...");
                viewer.ShowDialog();
            }
            catch(Exception ex)
            {
                var errorMessage = $"Failed to show file in viewer: {ex}";
                Log.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UI.VersionFilesStatusLabel.Text = $"Showing {UI.FileList.Items.Count} files.";
                UI.FileList.Enabled = true;
                _ = CleanupFileViewerResourcesAsync(
                    tempFile, 
                    versionFolder,
                    relativeEquipmentPath, 
                    explorerTransactionId.ToString()
                );
            }
        }

        private async Task CleanupFileViewerResourcesAsync(
            string localTempFile, 
            string versionFolder, 
            string relativeEquipmentPath,
            string explorerTransactionId)
        {
            Log.Debug("Cleaning up file viewer resources...");

            try
            {
                Log.Debug($"Deleting local temporary file: {localTempFile}");
                if (File.Exists(localTempFile))
                    File.Delete(localTempFile);

                if (versionFolder == "Latest")
                {
                    Log.Debug("File was extracted from Latest.tar package in remote, " +
                        $"deleting remote files which are no longer needed, explorerTransactionId = {explorerTransactionId}");
                    await mechSyncService.DeleteExplorerFilesAsync(relativeEquipmentPath, explorerTransactionId);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to cleanup file viewer resources: {ex.Message}";
                Log.Error(errorMessage);
            }
            Log.Debug("Gracefully cleaned up file viewer resources.");
        }

        private int GetFileGroupIndex(string filePath)
        {
            if (UI.FileList.Groups.Count != 3)
            {
                return -1;
            }
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".sldasm": return 0;
                case ".sldprt": return 1;
                case ".slddrw": return 2;
                default: return -1;
            }
        }

        private void ProjectList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Get the selected ListViewItem, if any
                ListViewItem selectedItem = UI.ProjectList.SelectedItems.Count > 0 ? UI.ProjectList.SelectedItems[0] : null;

                // Check if a ListViewItem was double-clicked
                if (selectedItem != null && selectedItem.Tag is AggregatedProjectDetails)
                {
                    var targetProject = selectedItem.Tag as AggregatedProjectDetails;
                    OpenProject(targetProject);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to open project: {ex}";
                Log.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Get the selected ListViewItem, if any
                ListViewItem selectedItem = UI.FileList.SelectedItems.Count > 0 ? UI.FileList.SelectedItems[0] : null;

                // Check if a ListViewItem was double-clicked
                if (selectedItem != null && selectedItem.Tag is FileMetadata)
                {
                    var targetFile = selectedItem.Tag as FileMetadata;
                    await ShowFileInViewerAsync(targetFile, UI.VersionSelector.Text);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to open project: {ex}";
                Log.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseProjectButton_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private async void RefreshFilesButton_Click(object sender, EventArgs e)
        {
            await RefreshVersionFilesAsync();
        }

        private async void VersionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefreshVersionFilesAsync();
        }

        private async Task AddProjectToListAsync(AggregatedProjectDetails projectDetails)
        {
            string latestVersionText;
            if (projectDetails.LatestVersion != null)
            {
                var latestVersionOwner = await RetrieveUserDetails(projectDetails.LatestVersion.Owner.UserId);
                latestVersionText = $"V{projectDetails.LatestVersion.Major} - {latestVersionOwner.FullName}";
            }
            else latestVersionText = "None";

            string ongoingVersionText;
            if (projectDetails.OngoingVersion != null)
            {
                var ongoingVersionOwner = await RetrieveUserDetails(projectDetails.OngoingVersion.Owner.UserId);
                ongoingVersionText = $"V{projectDetails.OngoingVersion.Major} - {ongoingVersionOwner.FullName}";
            }
            else ongoingVersionText = "None";

            var projectCreator = await RetrieveUserDetails(projectDetails.ProjectDetails.CreatedBy);

            var item = new ListViewItem(projectDetails.ToString());
            item.SubItems.Add(projectDetails.ProjectDetails.CreatedAt.ToString());
            item.SubItems.Add(projectCreator.FullName);
            item.SubItems.Add(latestVersionText);
            item.SubItems.Add(ongoingVersionText);
            item.Tag = projectDetails;
            item.ImageIndex = 0;
            UI.ProjectList.Items.Add(item);
        }
        
        private async void ProjectSearchFilter_TextChanged(object sender, EventArgs e)
        {
            await RefreshProjectListAsync(UI.ProjectSearchFilter.Text);
        }

        private async void FileSearchFilter_TextChanged(object sender, EventArgs e)
        {
            await RefreshVersionFilesAsync();
        }

        private async void FileSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                await RefreshVersionFilesAsync();
        }

        private async void ProjectSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                await RefreshProjectListAsync(UI.ProjectSearchFilter.Text != "" ? UI.ProjectSearchFilter.Text : null);
        }

        private async Task<UserDetails> RetrieveUserDetails(string userId)
        {
            UserDetails userDetails;

            if (userDetailsIndex.ContainsKey(userId))
            {
                userDetails = userDetailsIndex[userId];
            }
            else
            {
                userDetails = await authServiceClient.GetUserDetailsAsync(userId);
                userDetailsIndex.TryAdd(userId, userDetails);
            }
            return userDetails;
        }

    }
}
