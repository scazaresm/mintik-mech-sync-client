using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using System;
using System.Drawing;
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
            try
            {
                var response = await AuthenticationServiceClient.Instance.LoginAsync(new LoginRequest()
                {
                    Email = Email.Text,
                    Password = Password.Text
                });
                Hide();
                VersionSynchronizerForm.Instance.Show();
            }
            catch(UnauthorizedAccessException)
            {
                LoginErrorMessage.Visible = true;
                LoginErrorMessage.Text = "Invalid credentials.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.GetType(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
