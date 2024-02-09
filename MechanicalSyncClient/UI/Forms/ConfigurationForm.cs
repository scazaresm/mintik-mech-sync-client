using Serilog;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ConfigurationForm : Form
    {
        private readonly string SERVER_URL = "SERVER_URL";
        private readonly string DEFAULT_TIMEOUT_SECONDS = "DEFAULT_TIMEOUT_SECONDS";
        private readonly string WORKSPACE_DIRECTORY = "WORKSPACE_DIRECTORY";
        private readonly string EDRAWINGS_VIEWER_CLSID = "EDRAWINGS_VIEWER_CLSID";

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            LoadServerConfiguration();
            LoadSyncConfiguration();
        }

        private void LoadServerConfiguration()
        {
            try
            {
                RemoteServer.Text = ConfigurationManager.AppSettings[SERVER_URL] ?? "";
                var timeout = ConfigurationManager.AppSettings[DEFAULT_TIMEOUT_SECONDS] ?? "10";
                ServerTimeout.Value = decimal.Parse(timeout);

                ApplyServerChanges.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to load server configuration: {ex}");
                MessageBox.Show($"Failed to load server configuration: {ex}");
            }
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ApplyServerChanges_Click(object sender, EventArgs e)
        {
            try
            {
                UpsertSetting(SERVER_URL, RemoteServer.Text);
                UpsertSetting(DEFAULT_TIMEOUT_SECONDS, ServerTimeout.Value.ToString());

                ConfigurationManager.RefreshSection("appSettings");
                ApplyServerChanges.Enabled = false;
            }
            catch(Exception ex)
            {
                var message = $"Could not apply server config changes: ${ex}";
                Log.Error(message);
                MessageBox.Show(message);
            }
        }

        private void UpsertSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;

            if (settings[key] != null)
                settings[key].Value = value;
            else
                settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
        }

        private void RemoteServer_TextChanged(object sender, EventArgs e)
        {
            ApplyServerChanges.Enabled = ValidateServerSettings();
        }

        private void ServerTimeout_ValueChanged(object sender, EventArgs e)
        {
            ApplyServerChanges.Enabled = ValidateServerSettings();
        }

        private bool ValidateServerSettings()
        {
            return RemoteServer.Text != "" && ServerTimeout.Value >= 10;
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

        private void ApplySyncChanges_Click(object sender, EventArgs e)
        {
            try
            {
                UpsertSetting(WORKSPACE_DIRECTORY, WorkspaceDirectory.Text);
                UpsertSetting(EDRAWINGS_VIEWER_CLSID, EdrawingsViewerClsid.Text);

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

        private void EdrawingsViewerClsid_TextChanged(object sender, EventArgs e)
        {
            ApplySyncChanges.Enabled = ValidateSyncSettings();
        }
    }
}
