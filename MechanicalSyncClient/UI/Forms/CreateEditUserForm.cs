using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
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
    public partial class CreateEditUserForm : Form
    {
        private IAuthenticationServiceClient authenticationService = AuthenticationServiceClient.Instance;
        private UserDetails user = new UserDetails();

        public CreateEditUserForm(UserDetails editUser = null)
        {
            InitializeComponent();
            if (editUser != null)
                user = editUser;
            CreateLabel.Text = editUser == null ? "Create user" : "Edit user";
        }

        private void CreateEditUserForm_Load(object sender, EventArgs e)
        {
            Role.SelectedIndex = 0;
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void EmailConfirmation_TextChanged(object sender, EventArgs e)
        {
            user.Email = Email.Text;
            ValidateData();
        }

        private void ValidateData()
        {
            if (Email.Text != EmailConfirmation.Text && EmailConfirmation.Text.Length > 0)
            {
                ErrorMessage.Text = "Email confirmation must match.";
                ErrorMessage.Visible = true;
                CreateUserButton.Enabled = false;
                return;
            }
            ErrorMessage.Visible = false;

            var isValidData = 
                Email.Text.Length > 0 &&
                FirstName.Text.Length > 0 &&
                LastName.Text.Length > 0;

            CreateUserButton.Enabled = isValidData;
        }


        private void CancelCreateUserButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void CreateUserButton_Click(object sender, EventArgs e)
        {
            await authenticationService.RegisterUserAsync(new RegisterUserRequest()
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DisplayName = DisplayName.Text,
                Role = Role.Text,
                Enabled = Enabled.Checked
            });
            DialogResult = DialogResult.OK;
        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            user.FirstName = FirstName.Text.Trim();
            DisplayName.Text = user.FirstName;
            ValidateData();
        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            user.LastName = LastName.Text.Trim();
            ValidateData();
        }

        private void Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            user.Role = Role.Text;
            ValidateData();
        }

        private void Enabled_CheckedChanged(object sender, EventArgs e)
        {
            user.Enabled = Enabled.Checked;
            ValidateData();
        }

        private void DisplayName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }
    }
}
