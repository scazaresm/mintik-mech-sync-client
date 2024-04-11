using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
            Text = editUser == null ? "Create user" : "Edit user";
            
            Email.Text = editUser == null ? "" : editUser.Email;
            Email.Enabled = editUser == null;

            EmailConfirmation.Text = Email.Text;
            EmailConfirmation.Enabled = Email.Enabled;

            FirstName.Text = editUser == null ? "" : editUser.FirstName;
            LastName.Text = editUser == null ? "" : editUser.LastName;
            Role.SelectedIndex = editUser == null ? 0 : GetRoleIndex(editUser.Role);
            Enabled.Checked = editUser == null ? true : editUser.Enabled;
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
            // edit
            if (user != null)
            {
                await authenticationService.UpdateUser(
                    user.Id,
                    new UpdateUserRequest()
                    {
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        DisplayName = DisplayName.Text,
                        Role = Role.Text,
                        Enabled = Enabled.Checked,
                    }
                );
                DialogResult = DialogResult.OK;
                return;
            }

            // create
            await authenticationService.RegisterUserAsync(new RegisterUserRequest()
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DisplayName = DisplayName.Text,
                Role = Role.Text,
                Enabled = Enabled.Checked
            });

            MessageBox.Show(
                "The new user has been created and the first login instructions have been sent to his/her email.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
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
            if (user != null) return; // skip when edit

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

        private int GetRoleIndex(string role)
        {
            switch (role)
            {
                default:
                case "Designer": return 0;

                case "Viewer": return 1;
                case "Admin": return 2;
            }
        }
    }
}
