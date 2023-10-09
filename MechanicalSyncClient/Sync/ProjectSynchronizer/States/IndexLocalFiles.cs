using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class IndexLocalFiles : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            string[] allLocalFiles = Directory.GetFiles(Synchronizer.LocalProject.LocalDirectory, "*.*", SearchOption.AllDirectories);

            Synchronizer.LocalFileIndex.Clear();
            foreach (string fullFilePath in allLocalFiles)
            {
                string fileName = Path.GetFileName(fullFilePath);
                string relativeFilePath = fullFilePath.Replace(Synchronizer.LocalProject.LocalDirectory + Path.DirectorySeparatorChar, "");
                relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

                // ommit lock files and duplicates
                if (fileName.StartsWith("~$"))
                    continue;

                var fileChecksum = await new Sha256ChecksumCalculator().CalculateChecksumAsync(fullFilePath);
                var metadata = new FileMetadata()
                {
                    FileChecksum = fileChecksum,
                    RelativeFilePath = relativeFilePath
                };

                Synchronizer.LocalFileIndex.Add(relativeFilePath, metadata);
            }

            Synchronizer.SetState(new IndexRemoteFilesState());
            await Synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Indexing local files...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
