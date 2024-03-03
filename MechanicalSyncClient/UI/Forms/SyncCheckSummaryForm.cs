using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using Serilog;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class SyncCheckSummaryForm : Form
    {
        public bool OnlineWorkSummaryMode { get; set; } = false;

        private readonly IVersionSynchronizer versionSynchronizer;
        private readonly SyncCheckSummary summary;
        private readonly ILogger logger;
        private ListViewItem selectedItem;

        public SyncCheckSummaryForm(
            IVersionSynchronizer versionSynchronizer, 
            SyncCheckSummary summary, 
            ILogger logger
            )
        {
            InitializeComponent();
            this.versionSynchronizer = versionSynchronizer ?? throw new ArgumentNullException(nameof(versionSynchronizer));
            this.summary = summary ?? throw new ArgumentNullException(nameof(summary));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void SyncSummaryForm_Load(object sender, EventArgs e)
        {
            SummaryListView.Items.Clear();
            PopulateCreatedFiles();
            PopulateChangedFiles();
            PopulateDeletedFiles(); 
            
            if (OnlineWorkSummaryMode)
            {
                Text = "Online work summary";
                TitleLabel.Text = "The following changes were detected in your local copy while working online, and all of them were reflected in remote server:";
                SyncNowButton.Text = "OK";
                CancelSyncButton.Visible = false;
            }
        }

        private void PopulateCreatedFiles()
        {
            foreach (FileMetadata file in summary.CreatedFiles.Values)
            {
                var item = new ListViewItem(file.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.SubItems.Add("Add");
                item.ImageIndex = 0;
                item.Group = SummaryListView.Groups[0];
                item.Tag = file;
                SummaryListView.Items.Add(item);
            }
        }

        private void PopulateChangedFiles()
        {
            foreach (FileMetadata file in summary.ChangedFiles.Values)
            {
                var item = new ListViewItem(file.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.SubItems.Add("Update");
                item.ImageIndex = 1;
                item.Group = SummaryListView.Groups[1];
                item.Tag = file;
                SummaryListView.Items.Add(item);
            }
        }

        private void PopulateDeletedFiles()
        {
            foreach (FileMetadata file in summary.DeletedFiles.Values)
            {
                var item = new ListViewItem(file.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.SubItems.Add("Delete");
                item.ImageIndex = 2;
                item.Group = SummaryListView.Groups[2];
                item.Tag = file;
                SummaryListView.Items.Add(item);
            }
        }

        private void SyncNowButton_Click(object sender, EventArgs e)
        {
            SyncNowButton.Enabled = false;
            DialogResult = DialogResult.OK;
        }

        private void CancelSyncButton_Click(object sender, EventArgs e)
        {
            CancelSyncButton.Enabled = false;
            DialogResult = DialogResult.Cancel;
        }

        private void SummaryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompareButton.Enabled = IsCompareSelectedFileAllowed();

            if (SummaryListView.SelectedItems.Count > 0)
                selectedItem = SummaryListView.SelectedItems[0];
            else
                selectedItem = null;
        }

        private bool IsCompareSelectedFileAllowed()
        {
            return SummaryListView.SelectedItems.Count > 0;
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            logger.Debug("CompareButton has been clicked.");

            if (selectedItem == null || selectedItem.Tag == null)
            {
                logger.Debug("Either selectedItem or selectedItem.Tag are null, nothing to compare.");
                return;
            }

            CompareButton.Enabled = false;
            SummaryListView.Enabled = false;

            var localFileMetadata = selectedItem.Tag as FileMetadata;

            var remoteAction = selectedItem.SubItems.Count == 2 ? selectedItem.SubItems[1].Text : null;

            var cmd = new CompareDesignFileCommand(
                versionSynchronizer, 
                localFileMetadata, 
                remoteAction, 
                logger
                ) 
            {
                OnlineWorkSummaryMode = OnlineWorkSummaryMode
            };
            await cmd.RunAsync();
            
            SummaryListView.Enabled = true;
            CompareButton.Enabled = IsCompareSelectedFileAllowed();
        }

        private void SummaryListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Get the selected ListViewItem, if any
                ListViewItem selectedItem = SummaryListView.SelectedItems.Count > 0 ? SummaryListView.SelectedItems[0] : null;
                if (selectedItem == null) return;

                // Check if a ListViewItem was double-clicked
                var localVersionDirectory = versionSynchronizer.Version.LocalDirectory;

                // build local file path with local version directory and relative file path
                var localFilePath = Path.Combine(
                    localVersionDirectory,
                    selectedItem.SubItems[0].Text.Replace('/', Path.DirectorySeparatorChar)
                );

                if (!File.Exists(localFilePath))
                {
                    logger.Debug($"Trying to view a design file which no longer exists {localFilePath}, will do nothing.");
                    return;
                }

                var designViewerForm = new DesignFileViewerForm(localFilePath);
                designViewerForm.Initialize();
                designViewerForm.Show();
            }
            catch (COMException)
            {
                var errorMessage = "Failed to connect to eDrawings software. Please make sure you have it installed on your computer and set the correct EDRAWINGS_VIEWER_CLSID value in the config file.";
                logger.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to open design viewer: {ex}";
                logger.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
