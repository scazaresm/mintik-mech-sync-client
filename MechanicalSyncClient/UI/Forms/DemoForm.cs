using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Sync;
using MechanicalSyncApp.Sync.ProjectSynchronizer;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DemoForm : Form
    {
        private LocalProject project;
        private IProjectSynchronizer projectSynchronizer;

        public DemoForm()
        {
            InitializeComponent();
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void InitSynchronizerButton_Click(object sender, EventArgs e)
        {
            project = new LocalProject("651d7d0d05ed6cb587091598", @"C:\sync_demo");
            projectSynchronizer = new ProjectSynchronizer(project, new ProjectSynchronizerUI()
            {
                FileViewer = new FileViewer(new ListView(), project.LocalDirectory)
            });
        }

        private void StartMonitoringButton_Click(object sender, EventArgs e)
        {
            if (projectSynchronizer == null)
            {
                MessageBox.Show("Initialize the synchronizer first.");
                return;
            }
            projectSynchronizer.ChangeMonitor.StartMonitoring();
            ProcessEventsButton.Enabled = true;
        }

        private void StopMonitoringButton_Click(object sender, EventArgs e)
        {
            if (projectSynchronizer == null)
            {
                MessageBox.Show("Initialize the synchronizer first.");
                return;
            }
            projectSynchronizer.ChangeMonitor.StopMonitoring();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await AuthenticationServiceClient.Instance.LoginAsync(new LoginRequest()
                {
                    Username = "sergio@foo.com",
                    Password = "12345678"
                });
                MessageBox.Show(AuthenticationServiceClient.Instance.UserDetails.Username);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DownloadFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new DownloadFileRequest()
                {
                    LocalFilename = "M097_00-02-001.SLDPRT",
                    RelativeFilePath = "folder/M097_00-02-001.SLDPRT",
                    ProjectId = "651ca7196335322b03dc103d",
                    VersionFolder = "Ongoing"
                };
                await MechSyncServiceClient.Instance.DownloadFileAsync(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GetFilesMetadataButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new GetFilesMetadataRequest()
                {
                    ProjectId = "651ca7196335322b03dc103d",
                    VersionFolder = "Ongoing"
                };
                var response = await MechSyncServiceClient.Instance.GetFilesMetadataAsync(request);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void AnalyzeLocalButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new GetFilesMetadataRequest()
                {
                    ProjectId = project.RemoteId,
                    VersionFolder = "Ongoing"
                };
                var filesMetadataInRemote = await MechSyncServiceClient.Instance.GetFilesMetadataAsync(request);

                var analyzer = new LocalProjectAnalyzer(project, new Sha256ChecksumValidator());

                var result = analyzer.CompareAgainstRemote(filesMetadataInRemote.Files);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DownloadProgressButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new DownloadFileRequest()
                {
                    LocalFilename = "M097_00-00.SLDASM",
                    RelativeFilePath = "folder/M097_00-00.SLDASM",
                    ProjectId = "651ca7196335322b03dc103d",
                    VersionFolder = "Ongoing"
                };
                await MechSyncServiceClient.Instance.DownloadFileAsync(request, UpdateToolStripProgressBar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateToolStripProgressBar(int progressValue)
        {
            if (toolStripProgressBar1.Owner.InvokeRequired)
            {
                toolStripProgressBar1.Owner.Invoke(new Action(() => UpdateToolStripProgressBar(progressValue)));
            }
            else
            {
                toolStripProgressBar1.Value = progressValue;
            }
        }

        private async void UploadFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new UploadFileRequest()
                {
                    LocalFilePath = @"C:\Users\Sergio Cazares\Desktop\M097_00\79\M097_00-79.sldasm",
                    RelativeFilePath = "folder1/folder2/M097_00-79.sldasm",
                    ProjectId = "651653adad5cbc5699ddff66"
                };
                await MechSyncServiceClient.Instance.UploadFileAsync(request);
                MessageBox.Show("File uploaded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new DeleteFileRequest()
                {
                    RelativeFilePath = "folder1/folder2/M097_00-79.sldasm",
                    ProjectId = "651cd9b0ab729ebc4068d45d"
                };
                await MechSyncServiceClient.Instance.DeleteFileAsync(request);
                MessageBox.Show("File deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessEventsButton_Click(object sender, EventArgs e)
        {
            ProcessEventsButton.Enabled = false;
            projectSynchronizer.SetState(new SyncLocalProjectState());
            _ = projectSynchronizer.RunTransitionLogicAsync();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected ListViewItem, if any
            ListViewItem selectedItem = listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0] : null;

            // Check if a ListViewItem was double-clicked
            if (selectedItem != null)
            {
                // Do something with the double-clicked ListViewItem
                string text = selectedItem.Text;
                string subItemText = selectedItem.SubItems[2].Text;

                // Example: Show a message box with the item's text
                MessageBox.Show($"Item: {text}, Subitem: {subItemText}");
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            projectSynchronizer.UI.FileViewer.PopulateFiles();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            projectSynchronizer.UI.FileViewer = new FileViewer(listView1, project.LocalDirectory);
        }
    }
}
