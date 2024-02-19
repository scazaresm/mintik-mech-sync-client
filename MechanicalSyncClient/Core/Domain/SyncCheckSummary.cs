using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MechanicalSyncApp.Core.Domain
{
    public class SyncCheckSummary
    {
        public Dictionary<string, FileMetadata> CreatedFiles { get; set; }  // existing in local, not in remote
        public Dictionary<string, FileMetadata> DeletedFiles { get; set; }  // existing in remote, not in local
        public Dictionary<string, FileMetadata> ChangedFiles { get; set; }  // existing in both local and remote, but different (checksum mismatch)
        public Dictionary<string, FileMetadata> SyncedFiles { get; set; }   // existing in both local and remote, with exactly same checksum

        public Exception ExceptionObject { get; set; }

        public bool HasChanges {
            get
            {
                return ChangedFiles.Count > 0 || DeletedFiles.Count > 0 || CreatedFiles.Count > 0;
            }
        }

        public SyncCheckSummary()
        {
            SyncedFiles = new Dictionary<string, FileMetadata>();
            ChangedFiles = new Dictionary<string, FileMetadata>();
            DeletedFiles = new Dictionary<string, FileMetadata>();
            CreatedFiles = new Dictionary<string, FileMetadata>();
        }

        public void AddSyncedFile(FileMetadata syncedFile)
        {
            syncedFile.RelativeFilePath = syncedFile.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

            if (!SyncedFiles.ContainsKey(syncedFile.RelativeFilePath))
                SyncedFiles[syncedFile.RelativeFilePath] = syncedFile;
        }

        public void AddCreatedFile(FileMetadata createdFile)
        {
            createdFile.RelativeFilePath = createdFile.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

            // add as created
            if (!CreatedFiles.ContainsKey(createdFile.RelativeFilePath))
                CreatedFiles[createdFile.RelativeFilePath] = createdFile;

            // if file was previously deleted, remove it since it was created again
            if (DeletedFiles.ContainsKey(createdFile.RelativeFilePath))
                DeletedFiles.Remove(createdFile.RelativeFilePath);
        }

        public void AddChangedFile(FileMetadata changedFile)
        {
            changedFile.RelativeFilePath = changedFile.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

            // only add file as changed when it does not appear as created on this summary
            if (!ChangedFiles.ContainsKey(changedFile.RelativeFilePath) && 
                !CreatedFiles.ContainsKey(changedFile.RelativeFilePath)
            )
                ChangedFiles[changedFile.RelativeFilePath] = changedFile;
        }


        public void AddDeletedFile(FileMetadata deletedFile)
        {
            deletedFile.RelativeFilePath = deletedFile.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

            if (!DeletedFiles.ContainsKey(deletedFile.RelativeFilePath))
                DeletedFiles[deletedFile.RelativeFilePath] = deletedFile;

            if (CreatedFiles.ContainsKey(deletedFile.RelativeFilePath))
                CreatedFiles.Remove(deletedFile.RelativeFilePath);

            if (ChangedFiles.ContainsKey(deletedFile.RelativeFilePath))
                ChangedFiles.Remove(deletedFile.RelativeFilePath);
        }

        public string CalculateSyncChecksum()
        {
            // Combine FileChecksum values from SyncedFiles list
            StringBuilder combinedChecksum = new StringBuilder();
            foreach (var fileMetadata in SyncedFiles.Values)
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
