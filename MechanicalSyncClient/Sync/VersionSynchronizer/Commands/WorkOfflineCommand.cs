using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    internal class WorkOfflineCommand : VersionSynchronizerCommandAsync
    {
        public VersionSynchronizer Synchronizer { get; private set; }

        public WorkOfflineCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            var UI = Synchronizer.UI;

            UI.SynchronizerToolStrip.Enabled = true;
            UI.WorkOfflineButton.Visible = false;
            UI.WorkOnlineButton.Visible = true;
            UI.SyncRemoteButton.Visible = true;

            Synchronizer.ChangeMonitor.StopMonitoring();
            UI.FileViewer.PopulateFiles();

            Synchronizer.SetState(new IdleState());
            await Synchronizer.RunStepAsync();
        }
    }
}
