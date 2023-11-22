using MechanicalSyncApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IVersionChangeMonitor : IDisposable
    {
        void Initialize();
        void StartMonitoring();
        bool IsMonitoring();
        void StopMonitoring();
        bool IsEventQueueEmpty();
        FileSyncEvent PeekNextEvent();
        void EnqueueEvent(FileSyncEvent fileChangeEvent);
        FileSyncEvent DequeueEvent();
        long GetTotalInQueue();
    }
}
