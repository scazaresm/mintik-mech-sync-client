using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class LoginForm : Form
    {
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
                    Username = Username.Text,
                    Password = Password.Text
                });
                Hide();
                new MainForm().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.GetType(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
