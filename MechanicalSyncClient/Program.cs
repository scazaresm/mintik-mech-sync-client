using MechanicalSyncApp.UI.Forms;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.AppSettings()
               .CreateLogger();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = "C:\\Program Files\\Common Files\\eDrawings2023\\eDrawings.Interop.EModelViewControl.dll";
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
