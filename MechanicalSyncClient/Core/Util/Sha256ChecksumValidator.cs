using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class Sha256ChecksumValidator : IChecksumValidator
    {
        public string CalculateFromFile(string filePath)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public void ValidateFileChecksum(string filePath, string expectedChecksum)
        {
            string actualChecksum = CalculateFromFile(filePath);

            if (actualChecksum != expectedChecksum)
            {
                throw new ChecksumMismatchException("Checksum verification failed. The file may be corrupted.");
            }
        }
    }
}
