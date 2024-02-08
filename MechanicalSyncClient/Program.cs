using MechanicalSyncApp.UI.Forms;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("mech-sync.log",
                                rollingInterval: RollingInterval.Day,
                                flushToDiskInterval: TimeSpan.FromMinutes(1),
                                restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
