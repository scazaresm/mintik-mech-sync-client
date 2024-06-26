﻿using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ConnectionSettingsForm : Form
    {
        private readonly string SERVER_URL = "SERVER_URL";
        private readonly string DEFAULT_TIMEOUT_SECONDS = "DEFAULT_TIMEOUT_SECONDS";
        private readonly ILogger logger;

        public ConnectionSettingsForm(ILogger logger)
        {
            InitializeComponent();
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            LoadServerConfiguration();
        }

        private void LoadServerConfiguration()
        {
            try
            {
                RemoteServer.Text = Properties.Settings.Default.SERVER_URL ?? "";
                ServerTimeout.Value = Properties.Settings.Default.DEFAULT_TIMEOUT_SECONDS;

                ApplyServerChanges.Enabled = false;
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to load server configuration: {ex}");
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
                Properties.Settings.Default.SERVER_URL = RemoteServer.Text;
                Properties.Settings.Default.DEFAULT_TIMEOUT_SECONDS = (int)(ServerTimeout.Value);
                Properties.Settings.Default.Save();
                ApplyServerChanges.Enabled = false;
            }
            catch(Exception ex)
            {
                var message = $"Could not apply server config changes: ${ex}";
                logger.Error(message);
                MessageBox.Show(message);
            }
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

    }
}
