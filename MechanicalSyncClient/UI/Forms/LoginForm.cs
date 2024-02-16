using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using Serilog;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class LoginForm : Form
    {
        private bool isMouseDown = false;
        private Point firstPoint;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            LoginButton.Enabled = false;
            try
            {
                var response = await AuthenticationServiceClient.Instance.LoginAsync(new LoginRequest()
                {
                    Email = Email.Text,
                    Password = Password.Text
                });
                Hide();

                if (response.UserDetails.Role.ToLower() == "root")
                {
                    Log.Debug("Logged in as Root user, showing management console.");
                    ManagementConsoleForm.Instance.Show();
                    MessageBox.Show("You logged in as Root user.", "Root user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.UserDetails.HasInitialPassword)
                {
                    Log.Debug("User has initial password, showing form to change password.");
                    var setCustomPasswordForm = new ChangeYourPasswordForm();
                    setCustomPasswordForm.ShowDialog();
                    Show();
                }
                else if (response.UserDetails.Role == "Viewer")
                {
                    Log.Debug("User is Viewer, showing project explorer.");
                    var projectExplorerForm = new ProjectExplorerForm();
                    projectExplorerForm.ShowDialog();
                    Show();
                }
                else
                {
                    Log.Debug($"Successfully logged in with email {Email.Text}, showing version synchronzier form.");
                    VersionSynchronizerForm.Instance.Show();
                }
            }
            catch(UnauthorizedAccessException)
            {
                Log.Error($"Invalid credentials: {Email.Text}");
                LoginErrorMessage.Visible = true;
                LoginErrorMessage.Text = "Invalid credentials.";
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to login {ex.Message} {ex.GetType()} {ex.InnerException}");
                MessageBox.Show(ex.Message + " " + ex.GetType() + " " + ex.InnerException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoginButton.Enabled = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            LoginErrorMessage.Visible = false;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            LoginErrorMessage.Visible = false;
        }

        private void Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                LoginButton_Click(sender, e);
            }
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                firstPoint = e.Location;
                isMouseDown = true;
            }
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = Location.X - xDiff;
                int y = Location.Y - yDiff;
                Location = new Point(x, y);
            }
        }

        private void ConnectionSettingsButton_Click(object sender, EventArgs e)
        {
            var connectionSettingsForm = new ConnectionSettingsForm();
            connectionSettingsForm.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = AppVersion;
        }

        public string AppVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }
    }
}
