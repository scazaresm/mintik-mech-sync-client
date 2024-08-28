using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class PathUtils
    {
        public static string GetTempFileWithExtension(string extension)
        {
            string tempPath = Path.GetTempPath();
            string fileName = Guid.NewGuid().ToString() + extension;
            return Path.Combine(tempPath, fileName);
        }

        public static string GetTempFileWithName(string fileName)
        {
            string tempPath = Path.GetTempPath();
            return Path.Combine(tempPath, fileName);
        }
    }
}
