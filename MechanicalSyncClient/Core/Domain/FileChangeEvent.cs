using System;

namespace MechanicalSyncApp.Core.Domain
{
    public class FileChangeEvent
    {
        public LocalProject LocalProject { get; set; }

        public string RelativePath { get; set; }
        public string FullPath { get; set; }

        public DateTime RaiseDateTime { get; set; }
        public FileChangeEventType EventType { get; set; }
        public FileChangeEventState EventState { get; set; } = FileChangeEventState.Queued;
    }
}
