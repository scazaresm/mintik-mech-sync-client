using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
