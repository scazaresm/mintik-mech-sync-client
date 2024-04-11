using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DesignFilesAnalysisDialog : Form
    {
        private readonly CancellationTokenSource cts;
        public Dictionary<string, HashSet<string>> PartsInAssemblyLookup = new Dictionary<string, HashSet<string>>();

        public DesignFilesAnalysisDialog(CancellationTokenSource cts)
        {
            InitializeComponent();
            this.cts = cts ?? throw new ArgumentNullException(nameof(cts));
            TopMost = true;
        }

        public void SetProgress(int progress)
        {
            if (AnalysisProgressBar.InvokeRequired)
                AnalysisProgressBar.Invoke((MethodInvoker)(() => SetProgress(progress)));
            else
            {
                if (progress < 0) progress = 0;
                if (progress > 100) progress = 100;
                AnalysisProgressBar.Value = progress;
            }
        }

        public void SetStatus(string status)
        {
            if (StatusLabel.InvokeRequired)
                DetailsLabel.Invoke((MethodInvoker)(() => SetStatus(status)));
            else
                StatusLabel.Text = status ?? "";
        }

        public void SetDetails(string details)
        {
            if (DetailsLabel.InvokeRequired)
                DetailsLabel.Invoke((MethodInvoker)(() => SetDetails(details)));
            else
                DetailsLabel.Text = details ?? "";
        }

        private void CancelAnalysisButton_Click(object sender, EventArgs e)
        {
            CancelAnalysisButton.Enabled = false;
            SetDetails("Cancelling...");
            cts.Cancel();
            DialogResult = DialogResult.Cancel;
        }

        private void DesignFilesAnalysisDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
