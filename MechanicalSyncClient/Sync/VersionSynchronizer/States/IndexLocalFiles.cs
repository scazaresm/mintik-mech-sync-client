using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class IndexLocalFiles : VersionSynchronizerState
    {
        public override async Task RunAsync()
        {
            Log.Information("Indexing local files to compare with remote...");

            var indexer = new ConcurrentLocalFileIndexer(Synchronizer.Version.LocalDirectory, Synchronizer.FileExtensionFilter);
            indexer.ProgressChanged += Indexer_ProgressChanged;

            var ui = Synchronizer.UI;
            ui.SyncProgressBar.Visible = true;
            await indexer.IndexAsync();
            ui.SyncProgressBar.Visible = false;

            Synchronizer.LocalFileIndex = indexer.FileIndex;
            Log.Information("Completed local file index.");
        }

        private void Indexer_ProgressChanged(object sender, int progress)
        {
            var ui = Synchronizer.UI;

            ui.SynchronizerToolStrip.BeginInvoke((Action)(() =>
            {
                ui.SyncProgressBar.Value = progress;
            }));
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Indexing local files...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
