using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
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
    public partial class OpenVersionDialog : Form
    {
        public IVersionSynchronizer Synchronizer { get; }

        public OpenVersionDialog(IVersionSynchronizer synchronizer)
        {
            InitializeComponent();
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        private async void InitializeVersionSynchronizerForm_Load(object sender, EventArgs e)
        {
            OpeningLegend.Text = "Opening " + Synchronizer.Version;
            await OpenVersionAsync();
        }

        private async Task OpenVersionAsync()
        {
            try
            {
                await Synchronizer.OpenVersionAsync(Status, Progress);
                DialogResult = DialogResult.OK;
            }
            catch (VersionFolderAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);

                // abort if folder already exists and user didn't accept moving it to recycle bin
                DialogResult = DialogResult.Abort;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
