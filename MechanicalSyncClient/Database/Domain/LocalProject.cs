using System;
using System.Collections.Generic;

namespace MechanicalSyncApp.Database.Domain
{
    public class LocalProject
    {
        public int LocalId { get; set; }

        public string RemoteId { get; set; }
        public string FullPath { get; set; }

        public DateTime DownloadDateTime { get; set; }
        public DateTime LastSyncDateTime { get; set; }

        public List<FileSyncEvent> FileSyncEvents { get; set; }
    }
}

