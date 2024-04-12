using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private async void FrmAboutDialog_Load(object sender, EventArgs e)
        {
            MechanicalSyncLabel.Text = string.Empty;
            VersionLabel.Visible = false;
            await DisplayCharacterByCharacter("Mechanical Sync", MechanicalSyncLabel);
            VersionLabel.Text = AppVersion;
            VersionLabel.Visible = true;
        }

        private async Task DisplayCharacterByCharacter(string text, Label label)
        {
            foreach (char c in text)
            {
                label.Text += c;
                await Task.Delay(50);
            }
        }

        public string AppVersion
        {
            get
            {
                try
                {
                    if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                    {
                        Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                        return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                    }
                    else
                    {
                        var ver = Assembly.GetExecutingAssembly().GetName().Version;
                        return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed to retrieve deployment version: {ex.Message}");
                    return "Version: ?";
                }
            }
        }
    }
}
