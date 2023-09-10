using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core
{
    interface ILocalProjectMonitor : IDisposable
    {
        void OnFileCreated(object source, FileSystemEventArgs e);
        void OnFileDeleted(object source, FileSystemEventArgs e);
        void OnFileChanged(object source, FileSystemEventArgs e);
        void OnFileRenamed(object source, FileSystemEventArgs e);
    }
}
