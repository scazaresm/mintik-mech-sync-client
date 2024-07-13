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
                await Task.Run(() => Run());
            }
            finally
            {
                Synchronizer.UI.MarkFileAsFixedButton.Enabled = true;
            }
        }

        private void Run()
        {
            try
            {

                var metadata = Synchronizer.CurrentFileReviewTargetMetadata;
                var localFilePath = Path.Combine(
                    Synchronizer.Version.LocalDirectory,
                    metadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar)
                );

                logger.Debug("Preparing SW launcher...");

                var solidWorksPath = Settings.Default.SOLIDWORKS_EXE_PATH;
                string solidWorksLauncherPath = Path.Combine(Path.GetDirectoryName(solidWorksPath), "swShellFileLauncher.exe");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = solidWorksLauncherPath,
                    Arguments = $"\"{localFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                logger.Debug($"SW Launcher path: {solidWorksLauncherPath}");
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
