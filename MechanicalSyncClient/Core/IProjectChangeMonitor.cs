using MechanicalSyncApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IProjectChangeMonitor : IDisposable
    {
        bool IsMonitoring { get; }

        void StartMonitoring();
        void StopMonitoring();
        bool IsEventQueueEmpty();
        FileChangeEvent PeekNextEvent();
        void EnqueueEvent(FileChangeEvent fileChangeEvent);
        FileChangeEvent DequeueEvent();
    }
}
