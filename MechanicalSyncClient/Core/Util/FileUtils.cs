using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class FileUtils
    {
        static public async Task<FileInfo> GetFileInfoAsync(string filePath)
        {
            try
            {
                if(!File.Exists(filePath))
                    throw new FileNotFoundException();

                return await Task.Run(() =>
                {
                    return new FileInfo(filePath);
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
