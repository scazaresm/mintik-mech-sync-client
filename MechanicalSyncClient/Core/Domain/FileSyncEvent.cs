using System;

namespace MechanicalSyncApp.Core.Domain
{
    public class FileSyncEvent
    {
        public LocalVersion Version { get; set; }

        public string RelativeFilePath { get; set; }
        public string FullPath { get; set; }

        public DateTime RaiseDateTime { get; set; }
        public FileSyncEventType EventType { get; set; }
        public FileSyncEventState EventState { get; set; } = FileSyncEventState.Queued;
    }
}
