using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.Domain
{
    public class LocalProject
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }

        public int RemoteId { get; set; }
        public string FullPath { get; set; }

        public DateTime DownloadDateTime { get; set; }
        public DateTime LastSyncDateTime { get; set; }

        [OneToMany("LocalProjectId", "LocalProject")]
        public List<FileSyncEvent> FileSyncEvents { get; set; }
    }
}

