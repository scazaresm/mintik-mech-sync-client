using System;
using System.Threading;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DownloadWorkingCopyDialog : Form
    {
        private readonly CancellationTokenSource cts;

        public DownloadWorkingCopyDialog(CancellationTokenSource cts)
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

        private void DownloadWorkingCopyDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if user closed the dialog, then cancel the async task to download files (if not finished yet)
            cts.Cancel();
        }
    }
}
