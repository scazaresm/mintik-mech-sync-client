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

        private async Task PopulateUsers()
        {
            var allUsers = await authenticationService.GetAllUserDetailsAsync();

            UserList.Items.Clear();   
            foreach (var user in allUsers) 
            {
                var userItem = new ListViewItem(user.Email);
                userItem.SubItems.Add(user.FullName);
                userItem.SubItems.Add(user.DisplayName);
                userItem.SubItems.Add(user.Role);
                userItem.SubItems.Add(user.Enabled ? "Yes" : "No");
                UserList.Items.Add(userItem);
            }
        }

        private async void ManagementConsoleForm_Load(object sender, EventArgs e)
        {
            await PopulateUsers();
        }

        private async void AddUserButton_Click(object sender, EventArgs e)
        {
            var createUserForm = new CreateEditUserForm();
            
            if (createUserForm.ShowDialog() == DialogResult.OK) {
                MessageBox.Show(
                    "The new user has been created and the first login instructions have been sent to his/her email.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await PopulateUsers();
            }
        }

        private void ManagementConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await PopulateUsers();
        }
    }
}
