using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ManagementConsoleForm : Form
    {
        private readonly IAuthenticationServiceClient authenticationService = AuthenticationServiceClient.Instance;

        private readonly string WORKSPACE_DIRECTORY = "WORKSPACE_DIRECTORY";
        private readonly string EDRAWINGS_VIEWER_CLSID = "EDRAWINGS_VIEWER_CLSID";

        #region Singleton
        private static Form _instance = null;

        public static Form Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManagementConsoleForm();
                return _instance;
            }
        }

        private ManagementConsoleForm()
        {
            InitializeComponent();
        }
        #endregion

        private async Task PopulateUsers()
        {
            var allUsers = await authenticationService.GetAllUserDetailsAsync();

            UserList.Items.Clear();   
            foreach (var user in allUsers) 
            {
                var userItem = new ListViewItem(user.Email);
                userItem.SubItems.Add(user.FullName);
                userItem.SubItems.Add(user.DisplayName);
                userItem.SubItems.Add(user.Role);
                userItem.SubItems.Add(user.Enabled ? "Yes" : "No");
                UserList.Items.Add(userItem);
            }
        }

        private async void ManagementConsoleForm_Load(object sender, EventArgs e)
        {
            await PopulateUsers();
        }

        private async void AddUserButton_Click(object sender, EventArgs e)
        {
            var createUserForm = new CreateEditUserForm();
            
            if (createUserForm.ShowDialog() == DialogResult.OK) {
                MessageBox.Show(
                    "The new user has been created and the first login instructions have been sent to his/her email.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await PopulateUsers();
            }
        }

        private void ManagementConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await PopulateUsers();
        }

        private void LoadSyncConfiguration()
        {
            try
            {
                WorkspaceDirectory.Text = ConfigurationManager.AppSettings[WORKSPACE_DIRECTORY] ?? "";
                EdrawingsViewerClsid.Text = ConfigurationManager.AppSettings[EDRAWINGS_VIEWER_CLSID] ?? "";

                ApplySyncChanges.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to load server configuration: {ex}");
                MessageBox.Show($"Failed to load server configuration: {ex}");
            }
        }

        private void ApplySyncChanges_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsUtils.UpsertSetting(WORKSPACE_DIRECTORY, WorkspaceDirectory.Text);
                SettingsUtils.UpsertSetting(EDRAWINGS_VIEWER_CLSID, EdrawingsViewerClsid.Text);
                ConfigurationManager.RefreshSection("appSettings");
                ApplySyncChanges.Enabled = false;
            }
            catch (Exception ex)
            {
                var message = $"Could not apply sync config changes: ${ex}";
                Log.Error(message);
                MessageBox.Show(message);
            }
        }

        private bool ValidateSyncSettings()
        {
            return Directory.Exists(WorkspaceDirectory.Text) && EdrawingsViewerClsid.Text != "";
        }

        private void BrowseWorkspaceDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set the initial directory (optional)
                folderBrowserDialog.SelectedPath = "C:\\";

                // Show the dialog and check if the user clicked OK
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the selected folder path
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // Do something with the selected folder path, such as displaying it in a textbox
                    WorkspaceDirectory.Text = selectedFolderPath;
                }
            }
        }

        private void WorkspaceDirectory_TextChanged(object sender, EventArgs e)
        {
            ApplySyncChanges.Enabled = ValidateSyncSettings();
        }

        private void EdrawingsViewerClsid_TextChanged(object sender, EventArgs e)
        {
            ApplySyncChanges.Enabled = ValidateSyncSettings();
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Tabs.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    LoadSyncConfiguration();
                    break;
            }
        }
    }
}
