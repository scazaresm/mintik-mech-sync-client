using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
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
            var indexer = new ConcurrentLocalFileIndexer(Synchronizer.Version.LocalDirectory, Synchronizer.FileExtensionFilter);
            indexer.ProgressChanged += Indexer_ProgressChanged;

            var ui = Synchronizer.UI;
            ui.SyncProgressBar.Visible = true;
            await indexer.IndexAsync();
            ui.SyncProgressBar.Visible = false;

            Synchronizer.LocalFileIndex = indexer.FileIndex;
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
        }
    }
}
