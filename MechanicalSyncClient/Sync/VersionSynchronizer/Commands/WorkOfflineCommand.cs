using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    internal class WorkOfflineCommand : IVersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public WorkOfflineCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            var ui = Synchronizer.UI;

            ui.SynchronizerToolStrip.Enabled = true;
            ui.WorkOfflineButton.Visible = false;
            ui.WorkOnlineButton.Visible = true;
            ui.SyncRemoteButton.Visible = true;

            ui.PublishVersionButton.Enabled = true;
            ui.PublishVersionButton.Visible = true;

            ui.TransferOwnershipButton.Enabled = true;
            ui.TransferOwnershipButton.Visible = true;

            Synchronizer.ChangeMonitor.StopMonitoring();
            ui.FileViewer.PopulateFiles();

            Synchronizer.SetState(new IdleState());
            await Synchronizer.RunStepAsync();
        }
    }
}
