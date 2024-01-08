using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
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
    public partial class UserSelectorForm : Form
    {
        private readonly IAuthenticationServiceClient authenticationServiceClient = AuthenticationServiceClient.Instance;

        public UserDetails SelectedUserDetails { get; private set; }
        public bool IncludeMyself { get; set; } = false;

        private Dictionary<string, UserDetails> userLookup = new Dictionary<string, UserDetails>();

        public UserSelectorForm(string title, string message)
        {
            InitializeComponent();
            Text = title;
            MessageLabel.Text = message;
        }

        public UserSelectorForm()
        {
            InitializeComponent();
            Text = "Select user";
            MessageLabel.Text = "Select a user:";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var selectedUserEmail = UsersList.SelectedItems[0].SubItems[1].Text;
            SelectedUserDetails = userLookup[selectedUserEmail];
            DialogResult = DialogResult.OK;
        }

        private void UserSelectorForm_Load(object sender, EventArgs e)
        {
            PopulateUsers();
        }

        private async void PopulateUsers()
        {
            var allUsers = await authenticationServiceClient.GetAllUserDetailsAsync();
            var searchTarget = SearchFilter.Text.ToLower();

            var filter = searchTarget == null || searchTarget.Length == 0 
                ? allUsers 
                : allUsers.Where(user => 
                    user.FirstName.ToLower().Contains(searchTarget) ||
                    user.LastName.ToLower().Contains(searchTarget) ||
                    user.FullName.ToLower().Contains(searchTarget) ||
                    user.Email.ToLower().Contains(searchTarget)
                  ).ToList();

            UsersList.SelectedItems.Clear();
            UsersList.Items.Clear();
            userLookup.Clear();
            foreach (var user in filter)
            {
                if (!IncludeMyself && user.Id == authenticationServiceClient.LoggedUserDetails.Id)
                    continue;

                var item = new ListViewItem(user.FullName);
                item.SubItems.Add(user.Email);
                UsersList.Items.Add(item);
                userLookup.Add(user.Email, user);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SearchFilter_TextChanged(object sender, EventArgs e)
        {
            PopulateUsers();
        }

        private void UsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = UsersList.SelectedItems.Count > 0;
        }
    }
}
