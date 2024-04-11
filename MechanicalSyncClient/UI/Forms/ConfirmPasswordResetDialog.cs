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
    public partial class ConfirmPasswordResetDialog : Form
    {
        private readonly UserDetails userDetails;

        public ConfirmPasswordResetDialog(UserDetails userDetails)
        {
            InitializeComponent();
            this.userDetails = userDetails ?? throw new ArgumentNullException(nameof(userDetails));
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UserEmailConfirmation_TextChanged(object sender, EventArgs e)
        {
            ConfirmButton.Enabled = UserEmail.Text == UserEmailConfirmation.Text;
        }

        private void ConfirmPasswordResetDialog_Load(object sender, EventArgs e)
        {
            UserEmail.Text = userDetails.Email;
            UserEmailConfirmation.Focus();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
