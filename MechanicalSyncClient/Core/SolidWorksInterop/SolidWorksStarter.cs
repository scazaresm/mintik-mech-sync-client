using Serilog;
using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public class SolidWorksStarter : ISolidWorksStarter
    {
        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public int SolidWorksStartTimeoutSeconds { get; set; } = 60;
        public string SolidWorksExePath { get; set; } = @"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\SLDWORKS.exe";

        private Process process = null;
        private IEnumMoniker monikers = null;
        private IBindCtx context = null;
        private IRunningObjectTable rot = null;

        public ISldWorks SolidWorksApp { get; private set; }

        public bool Hidden { get; set; } = true;

        public bool ShowSplash { get; set; } = false;

        private readonly ILogger logger;

        private bool isLoaded = false;

        private bool disposedValue;

        public SolidWorksStarter(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartSolidWorksAsync()
        {
            await Task.Run(() => StartSolidWorks());
        }

        public void StartSolidWorks()
        {
            logger.Debug("StartSolidWorks begins.");

            if (!File.Exists(SolidWorksExePath))
                throw new FileNotFoundException($"The SolidWorks .exe file could not be found at {SolidWorksExePath}");

            var timeout = TimeSpan.FromSeconds(SolidWorksStartTimeoutSeconds);
            var startTime = DateTime.Now;

            logger.Debug("Preparing process start info...");
            var processInfo = new ProcessStartInfo()
            {
                FileName = SolidWorksExePath,
                Arguments = ShowSplash ? "" : "/r",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            logger.Debug("Preparing on idle notify event handler...");
            var onIdleFunc = new DSldWorksEvents_OnIdleNotifyEventHandler(() =>
            {
                isLoaded = true;
                return 0;
            });

            logger.Debug("Starting SolidWorks process...");
            process = Process.Start(processInfo);

            logger.Debug("Waiting for SolidWorks to finish loading...");
            while (!isLoaded)
            {
                if (DateTime.Now - startTime > timeout)
                    throw new TimeoutException("SolidWork process start has timed out.");

                if (SolidWorksApp == null)
                {
                    SolidWorksApp = GetAppFromProcess(process.Id);

                    if (SolidWorksApp != null)
                    {
                        (SolidWorksApp as SldWorks).OnIdleNotify += onIdleFunc;
                        logger.Debug("Waiting for SolidWorks initialization...");
                    }
                    Task.Delay(1000);
                }
            }
            logger.Debug("SolidWorks has been succesfully loaded!");

            if (Hidden && SolidWorksApp != null)
                ShowWindow(new IntPtr(SolidWorksApp.IFrameObject().GetHWnd()), 0);

            (SolidWorksApp as SldWorks).OnIdleNotify -= onIdleFunc;
        }

        private void ReleaseComObjects()
        {
            if (monikers != null)
                Marshal.ReleaseComObject(monikers);

            if (rot != null)
                Marshal.ReleaseComObject(rot);

            if (context != null)
                Marshal.ReleaseComObject(context);
        }

        private ISldWorks GetAppFromProcess(int processId)
        {
            var monikerName = $"SolidWorks_PID_{processId}";

            logger.Debug($"Searching process with moniker {monikerName}...");

            CreateBindCtx(0, out context);

            logger.Debug("Getting all running monikers from ROT table...");
            context.GetRunningObjectTable(out rot);
            rot.EnumRunning(out monikers);

            var moniker = new IMoniker[1];

            logger.Debug("Iterating through all monikers...");
            while (monikers.Next(1, moniker, IntPtr.Zero) == 0)
            {
                var curMoniker = moniker.First();

                string name = null;

                if (curMoniker != null)
                {
                    try
                    {
                        curMoniker.GetDisplayName(context, null, out name);
                        logger.Debug($"Current moniker: {name}");
                    }
                    catch (UnauthorizedAccessException) { }
                }

                if (string.Equals(monikerName, name, StringComparison.CurrentCultureIgnoreCase))
                {
                    logger.Debug("Found SolidWorks moniker, getting App object from ROT...");
                    rot.GetObject(curMoniker, out object app);
                    return app as ISldWorks;
                }
                Task.Delay(1000);
            }
            logger.Debug("Could not find SolidWorks App.");
            return null;
        }

        #region Dispose pattern

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                logger.Debug("SolidWorksStarter instance is being disposed...");
                if (disposing)
                {
                    logger.Debug("Releasing COM objects...");
                    ReleaseComObjects();

                    logger.Debug("Closing SolidWorks App...");
                    SolidWorksApp?.ExitApp();

                    logger.Debug("Killing and disposing process...");
                    process?.Kill();
                    process?.Dispose();
                }
                process = null;
                monikers = null;
                context = null;
                rot = null;

                disposedValue = true;
                logger.Debug("Successfully disposed SolidWorksStarter instance.");
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
