using System;

namespace MechanicalSyncApp.Database.Domain
{
    public class FileSyncEvent
    {
        public int Id { get; set; }

        public int LocalProjectId { get; set; }

        public LocalProject LocalProject { get; set; }

        public string RelativePath { get; set; }
        public string FullPath { get; set; }

        public DateTime RaiseDateTime { get; set; }
        public FileSyncEventType EventType { get; set; }
        public FileSyncEventState EventState { get; set; } = FileSyncEventState.Queued;
    }
}
