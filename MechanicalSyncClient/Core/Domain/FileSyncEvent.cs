using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.Domain
{
    public class FileSyncEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(LocalProject))]
        public int LocalProjectId { get; set; }

        [ManyToOne("LocalProjectId", "FileSyncEvents")]
        public LocalProject LocalProject { get; set; }

        public string Name { get; set; }
        public string FullPath { get; set; }
        
        public DateTime RaiseDateTime  { get; set; }
        public FileSyncEventType EventType { get; set; }
        public FileSyncEventState EventState { get; set; } = FileSyncEventState.Queued;
    }
}
