using MechanicalSyncApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IProjectMonitor : IDisposable
    {
        void StartMonitoring();
        void StopMonitoring();
        bool IsEventQueueEmpty();
        FileSyncEvent PeekNextEvent();
        FileSyncEvent DequeueEvent();
    }
}
