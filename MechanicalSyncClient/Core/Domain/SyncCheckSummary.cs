using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public class SyncCheckSummary
    {
        public List<FileMetadata> CreatedFiles { get; set; }  // existing in local, not in remote
        public List<FileMetadata> DeletedFiles { get; set; }  // existing in remote, not in local
        public List<FileMetadata> ChangedFiles { get; set; }  // existing in both local and remote, but different (checksum mismatch)
        public List<FileMetadata> SyncedFiles { get; set; }   // existing in both local and remote, with exactly same checksum

        public bool HasChanges {
            get
            {
                return ChangedFiles.Count > 0 || DeletedFiles.Count > 0 || CreatedFiles.Count > 0;
            }
        }

        public SyncCheckSummary()
        {
            SyncedFiles = new List<FileMetadata>();
            ChangedFiles = new List<FileMetadata>();
            DeletedFiles = new List<FileMetadata>();
            CreatedFiles = new List<FileMetadata>();
        }

        public string CalculateSyncChecksum()
        {
            // Combine FileChecksum values from SyncedFiles list
            StringBuilder combinedChecksum = new StringBuilder();
            foreach (var fileMetadata in SyncedFiles)
                combinedChecksum.Append(fileMetadata.FileChecksum);

            // Calculate SHA-256 hash of the combined checksum values
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] combinedChecksumBytes = Encoding.UTF8.GetBytes(combinedChecksum.ToString());
                byte[] hashBytes = sha256.ComputeHash(combinedChecksumBytes);

                // Convert the hash bytes to a lowercase hexadecimal string
                string hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hexString;
            }
        }
    }
}
