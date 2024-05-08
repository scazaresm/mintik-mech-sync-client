using MechanicalSyncApp.Core.Services.Authentication;
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
    public partial class ArchiveVersionWarningDialog : Form
    {
        public ArchiveVersionWarningDialog()
        {
            InitializeComponent();
        }

        private void Agree_CheckedChanged(object sender, EventArgs e)
        {
            ContinueButton.Enabled = (sender as CheckBox).Checked;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CancelArchivingButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
