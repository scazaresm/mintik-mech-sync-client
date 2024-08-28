using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ManagementConsoleForm : Form
    {
        private readonly IAuthenticationServiceClient authenticationService = AuthenticationServiceClient.Instance;

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

        private async Task PopulateUsersAsync(string filter = null)
        {
            var allUsers = await authenticationService.GetAllUserDetailsAsync();

            if (filter != null)
                allUsers = allUsers.Where((u) =>
                    u.FullName.Contains(filter) ||
                    u.Email.Contains(filter) ||
                    u.FirstName.Contains(filter) ||
                    u.LastName.Contains(filter) ||
                    u.DisplayName.Contains(filter)
                ).ToList();

            UserList.Items.Clear();   
            foreach (var user in allUsers) 
            {
                var userItem = new ListViewItem(user.Email);
                userItem.SubItems.Add(user.FullName);
                userItem.SubItems.Add(user.DisplayName);
                userItem.SubItems.Add(user.Role);
                userItem.SubItems.Add(user.Enabled ? "Yes" : "No");
                userItem.Tag = user;
                UserList.Items.Add(userItem);
            }
        }

        private async void ManagementConsoleForm_Load(object sender, EventArgs e)
        {
            await PopulateUsersAsync();
        }

        private async void AddUserButton_Click(object sender, EventArgs e)
        {
            var createUserForm = new CreateEditUserForm();
            
            if (createUserForm.ShowDialog() == DialogResult.OK) {
                await PopulateUsersAsync();
            }
        }

        private void ManagementConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await PopulateUsersAsync();
        }

        private void LoadSyncConfiguration()
        {
            try
            {
                WorkspaceDirectory.Text = Properties.Settings.Default.WORKSPACE_DIRECTORY ?? string.Empty;
                PublishingDirectory.Text = Properties.Settings.Default.PUBLISHING_DIRECTORY ?? string.Empty;
                EdrawingsViewerClsid.Text = Properties.Settings.Default.EDRAWINGS_VIEWER_CLSID ?? string.Empty;
                SolidWorksExePath.Text = Properties.Settings.Default.SOLIDWORKS_EXE_PATH ?? string.Empty;
                eDrawingsExePath.Text = Properties.Settings.Default.EDRAWINGS_EXE_PATH ?? string.Empty;

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
                Properties.Settings.Default.WORKSPACE_DIRECTORY = WorkspaceDirectory.Text;
                Properties.Settings.Default.EDRAWINGS_VIEWER_CLSID = EdrawingsViewerClsid.Text;
                Properties.Settings.Default.SOLIDWORKS_EXE_PATH = SolidWorksExePath.Text;
                Properties.Settings.Default.PUBLISHING_DIRECTORY = PublishingDirectory.Text;
                Properties.Settings.Default.EDRAWINGS_EXE_PATH = eDrawingsExePath.Text;
                Properties.Settings.Default.Save();

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
            return 
                Directory.Exists(WorkspaceDirectory.Text) && 
                Directory.Exists(PublishingDirectory.Text) &&
                EdrawingsViewerClsid.Text != "" &&
                File.Exists(SolidWorksExePath.Text) &&
                File.Exists(eDrawingsExePath.Text);
        }

        private void BrowseWorkspaceDirectory_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = "C:\\";

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    WorkspaceDirectory.Text = selectedFolderPath;
                }
            }
        }

        private void BrowseSolidWorksExePath_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    SolidWorksExePath.Text = openFileDialog.FileName;
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

        private void SolidWorksExePath_TextChanged(object sender, EventArgs e)
        {
            ApplySyncChanges.Enabled = ValidateSyncSettings();
        }

        private void PublishingDirectory_TextChanged(object sender, EventArgs e)
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

        private void BrowsePublishingDirectory_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = "C:\\";

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    PublishingDirectory.Text = selectedFolderPath;
                }
            }
        }

        private async void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            var selectedUser = UserList.SelectedItems[0].Tag as UserDetails;

            var confirmation = new ConfirmPasswordResetDialog(selectedUser).ShowDialog();

            EditUserButton.Enabled = false;
            ResetPasswordButton.Enabled = false;

            if (confirmation != DialogResult.OK) return;

            await authenticationService.ResetPasswordAsync(selectedUser.Id);

            MessageBox.Show(
                "Password reset succeeded!", 
                "Success", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );
        }

        private void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (sender as ListView);
            var isItemSelected =
                list.SelectedItems != null && list.SelectedItems.Count == 1;

            ResetPasswordButton.Enabled = isItemSelected;
            EditUserButton.Enabled = isItemSelected;
        }

        private async void FilterUserTextBox_TextChanged(object sender, EventArgs e)
        {
            await PopulateUsersAsync(FilterUserTextBox.Text == "" ? null : FilterUserTextBox.Text);
        }

        private async void EditUserButton_Click(object sender, EventArgs e)
        {
            var selectedUser = UserList.SelectedItems[0].Tag as UserDetails;

            var editUserForm = new CreateEditUserForm(selectedUser);

            if (editUserForm.ShowDialog() == DialogResult.OK)
            {
                await PopulateUsersAsync();
                EditUserButton.Enabled = false;
                ResetPasswordButton.Enabled = false;
            }
        }

        private void BrowseEDrawingsExePath_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    eDrawingsExePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void eDrawingsExePath_TextChanged(object sender, EventArgs e)
        {
            ApplySyncChanges.Enabled = ValidateSyncSettings();
        }
    }
}
