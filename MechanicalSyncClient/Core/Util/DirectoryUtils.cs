using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class DirectoryUtils
    {

        public static void SafeDeleteTempDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            // Get all files in the directory
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            // Remove read-only attribute from all files
            foreach (string file in files)
            {
                FileAttributes attributes = File.GetAttributes(file);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(file, attributes & ~FileAttributes.ReadOnly);
            }

            // Delete the directory
            Directory.Delete(directoryPath, true);
        }

    }
}
