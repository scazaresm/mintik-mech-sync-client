using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class CloseVersionCommand : VersionSynchronizerCommandAsync
    {
        public VersionSynchronizer Synchronizer {  get; set; }

        public CloseVersionCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            var UI = Synchronizer.UI;

            if (Synchronizer.ChangeMonitor.IsMonitoring())
            {
                var response = MessageBox.Show(
                    "You are currently working online on this version, are you sure to go offline and close it?",
                    "Close version",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (response != DialogResult.Yes)
                    return;

                await Synchronizer.WorkOfflineAsync();
            }
            Synchronizer.Dispose();
            UI.MainSplitContainer.Panel2Collapsed = true;
            UI.MainSplitContainer.Panel1Collapsed = false;
        }
    }
}
