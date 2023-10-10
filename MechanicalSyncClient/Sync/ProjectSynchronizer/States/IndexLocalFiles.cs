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
        private int indexedFiles = 0;
        private int totalFiles = 0;

        public override async Task RunAsync()
        {
            Synchronizer.UI.StatusLabel.Text = "Listing files in local directory...";
            string[] allLocalFiles = Directory.GetFiles(Synchronizer.LocalProject.LocalDirectory, "*.*", SearchOption.AllDirectories);
            totalFiles = allLocalFiles.Length;
            indexedFiles = 0;

            Synchronizer.UI.SyncProgressBar.Visible = true;
            Synchronizer.UI.SyncProgressBar.Value = 0;
            Synchronizer.LocalFileIndex.Clear();
            foreach (string fullFilePath in allLocalFiles)
            {
                string fileName = Path.GetFileName(fullFilePath);
                string relativeFilePath = fullFilePath.Replace(Synchronizer.LocalProject.LocalDirectory + Path.DirectorySeparatorChar, "");
                relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

                Synchronizer.UI.StatusLabel.Text = $"Indexing {relativeFilePath}";

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
                indexedFiles++;

                int progress = (int)((double)indexedFiles / totalFiles * 100.0);
                if (progress >= 0 && progress <= 100)
                    Synchronizer.UI.SyncProgressBar.Value = progress;
            }
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
        }
    }
}
