using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class OpenVersionProgressDialog : Form
    {
        private readonly CancellationTokenSource cts;

        public OpenVersionProgressDialog(CancellationTokenSource cts)
        {
            InitializeComponent();
            this.cts = cts ?? throw new ArgumentNullException(nameof(cts));
        }

        public void SetProgress(int progress)
        {
            Progress.Value = progress;
        }

        public void SetOpeningLegend(string legend) 
        { 
            OpeningLegend.Text = legend;
        }

        public void SetStatus(string status)
        {
            Status.Text = status;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // if user clicked on cancel button, then cancel the async task to download files
            cts.Cancel();
            DialogResult = DialogResult.Abort;
        }

        private void OpenVersionProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if user closed the dialog, then cancel the async task to download files (if not finished yet)
            cts.Cancel();
        }
    }
}
