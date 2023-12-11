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
            var allUsers = await AuthenticationServiceClient.Instance.GetAllUserDetailsAsync();

            UsersList.Items.Clear();
            foreach (var user in allUsers)
            {
                if (!IncludeMyself && user.Id == AuthenticationServiceClient.Instance.UserDetails.Id)
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
    }
}
