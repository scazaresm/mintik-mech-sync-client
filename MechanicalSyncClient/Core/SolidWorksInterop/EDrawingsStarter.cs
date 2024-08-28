using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public class EDrawingsStarter : IEDrawingsStarter
    {
        public string Output { get; private set; }
        public string Error { get; private set; }

        public void Start(string filePath)
        {
            var eDrawingsExePath = Properties.Settings.Default.EDRAWINGS_EXE_PATH;

            if (string.IsNullOrEmpty(eDrawingsExePath))
            {
                eDrawingsExePath = @"C:\Program Files\Common Files\eDrawings2023\eDrawings.exe";
            }

            if (!File.Exists(eDrawingsExePath))
            {
                throw new InvalidOperationException($"could not find the eDrawings executable at {eDrawingsExePath}");
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = eDrawingsExePath,
                Arguments = $"\"{filePath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                Output = process.StandardOutput.ReadToEnd();
                Error = process.StandardError.ReadToEnd();
            }
        }
    }
}
