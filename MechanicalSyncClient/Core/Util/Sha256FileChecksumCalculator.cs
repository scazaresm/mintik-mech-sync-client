using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class Sha256FileChecksumCalculator : IFileChecksumCalculator
    {
        public string CalculateChecksum(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(fileStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }

        public async Task<string> CalculateChecksumAsync(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                return await Task.Run(() =>
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        var hash = sha256.ComputeHash(fileStream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLower();
                    }
                });
            }
        }
    }
}
