using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Properties;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenFileForFixCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; }


        public OpenFileForFixCommand(IVersionSynchronizer versionSynchronizer, ILogger logger)
        {
            Synchronizer = versionSynchronizer ?? throw new ArgumentNullException(nameof(versionSynchronizer));
            this.logger = logger;
        }

        public async Task RunAsync()
        {
            try
            {
                Synchronizer.UI.MarkFileAsFixedButton.Enabled = false;

                var metadata = Synchronizer.CurrentFileReviewTargetMetadata;
                var localFilePath = Path.Combine(
                    Synchronizer.Version.LocalDirectory,
                    metadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar)
                );

                var solidWorksPath = Settings.Default.SOLIDWORKS_EXE_PATH;
                string swLauncherPath = Path.Combine(Path.GetDirectoryName(solidWorksPath), "swShellFileLauncher.exe");

                await Task.Run(() => Run(metadata, localFilePath, swLauncherPath, logger));
            }
            finally
            {
                Synchronizer.UI.MarkFileAsFixedButton.Enabled = true;
            }
        }

        private void Run(FileMetadata metadata, string localFilePath, string swLauncherPath, ILogger logger)
        {
            try
            {
                logger.Debug("Preparing SW launcher...");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = swLauncherPath,
                    Arguments = $"\"{localFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                logger.Debug($"SW Launcher path: {swLauncherPath}");
                logger.Debug($"Target file: {localFilePath}");

                logger.Debug("Initializing SW process with launcher...");
                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    logger.Debug($"Process launched with SW launcher has exited: {output} {error}");
                }
            }
            catch (Exception ex)
            {
                var message = $"Could not open file: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
