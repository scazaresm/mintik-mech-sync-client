using MechanicalSyncApp.Core;
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

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = solidWorksPath,
                    Arguments = $"\"{localFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    logger.Debug($"Process launched with SW launcher has exited: {output} {error}");
                }
            }
            catch (Exception ex)
            {
                var message = $"Could not open file for fix: {ex.Message} {ex?.InnerException?.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Synchronizer.UI.MarkFileAsFixedButton.Enabled = true;
            }
        }
    }
}
