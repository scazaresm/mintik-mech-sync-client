using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
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
    public partial class PublishVersionVerificationDialog : Form
    {
        private readonly IVersionSynchronizer synchronizer;

        public PublishVersionVerificationDialog(IVersionSynchronizer synchronizer)
        {
            InitializeComponent();
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        private void PublishVersionConfirmationDialog_Load(object sender, EventArgs e)
        {
            VersionNameLabel.Text = synchronizer.Version.ToString();
        }

        private void PublishNowButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CancelPublishButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
