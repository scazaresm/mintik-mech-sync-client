using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
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
    public partial class ChangeYourPasswordForm : Form
    {
        IAuthenticationServiceClient authenticationService = AuthenticationServiceClient.Instance;

        public ChangeYourPasswordForm()
        {
            InitializeComponent();
        }

        private void NewPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void ConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void ValidateData()
        {
            ChangeButton.Enabled = 
                NewPassword.Text.Length > 0 && 
                ConfirmPassword.Text.Length > 0 &&
                NewPassword.Text == ConfirmPassword.Text;
        }

        private async void ChangeButton_Click(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;

            if ( NewPassword.Text != ConfirmPassword.Text )
            {
                ErrorMessage.Text = "Password confirmation failed, make sure you enter the same value in both entries.";
                ErrorMessage.Visible = true;
            }
            await authenticationService.ChangeInitialPasswordAsync(ConfirmPassword.Text);
            DialogResult = DialogResult.OK;
        }

        private void CancelChangeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
