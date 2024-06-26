﻿using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class CloseVersionCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer {  get; set; }

        public CloseVersionCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting CloseVersionCommand...");

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
            Synchronizer = null;

            UI.VersionMenu.Visible = false;
            UI.ShowWorkspaceExplorer();
            await UI.WorkspaceTreeView.Refresh();

            logger.Debug("Completed CloseVersionCommand.");
        }
    }
}
