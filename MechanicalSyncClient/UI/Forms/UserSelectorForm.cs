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

        public UserSelectorForm()
        {
            InitializeComponent();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SelectedUserDetails = new UserDetails()
            {
                Username = textBox1.Text,
            };
        }
    }
}
