using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace MechanicalSyncApp.Sync
{
    class ProjectChangeMonitor : IProjectChangeMonitor
    {
        private FileSystemWatcher watcher;
        private bool disposedValue;

        public LocalProject Project { get; }
        public string FileFilter { get; }

        private readonly object eventQueueLock = new object();
        public Queue<FileSyncEvent> ChangeEventQueue { get; }

        public bool IsMonitoring { get; private set; }

        public ProjectChangeMonitor(LocalProject project, string fileFilter)
        {
            if (project is null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrEmpty(fileFilter))
            {
                throw new ArgumentException($"'{nameof(fileFilter)}' cannot be null or empty.", nameof(fileFilter));
            }

            Project = project;
            FileFilter = fileFilter;
            ChangeEventQueue = new Queue<FileSyncEvent>();

            InitializeFileSystemWatcher();
        }

        public bool IsEventQueueEmpty()
        {
            lock (eventQueueLock)
            {
                return ChangeEventQueue.Count == 0;
            }
        }

        public FileSyncEvent PeekNextEvent()
        {
            lock (eventQueueLock)
            {
                if (ChangeEventQueue.Count == 0)
                    return null;

                return ChangeEventQueue.Peek();
            }
        }

        public void EnqueueEvent(FileSyncEvent e)
        {
            // skip lock files (used by apps to indicate a given file is in use)
            if (Path.GetFileName(e.FullPath).Trim().StartsWith("~$"))
                return;

            lock (eventQueueLock)
            {
                ChangeEventQueue.Enqueue(e);
            }
        }

        public FileSyncEvent DequeueEvent()
        {
            lock (eventQueueLock)
            {
                if (ChangeEventQueue.Count == 0)
                    return null;

                return ChangeEventQueue.Dequeue();
            }
        }

        public long GetTotalInQueue()
        {
            lock (eventQueueLock)
            {
                return ChangeEventQueue.Count;
            }
        }

        public void StartMonitoring()
        {
            watcher.EnableRaisingEvents = true;
            IsMonitoring = true;
        }

        public void StopMonitoring()
        {
            watcher.EnableRaisingEvents = false;
            IsMonitoring = false;
        }

        private void InitializeFileSystemWatcher()
        {
            watcher = new FileSystemWatcher()
            {
                Path = Project.LocalDirectory,
                IncludeSubdirectories = true
            };
            watcher.Created += new FileSystemEventHandler(OnFileCreated);
            watcher.Deleted += new FileSystemEventHandler(OnFileDeleted);
            watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
            watcher.Changed += new FileSystemEventHandler(OnFileChanged);
        }




        #region FileSystemWatcher event callbacks
        private void OnFileCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File created: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProject = Project,
                EventType = FileSyncEventType.Created,
                RelativePath = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        private void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File deleted: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProject = Project,
                EventType = FileSyncEventType.Deleted,
                RelativePath = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        private void OnFileChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File changed: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProject = Project,
                EventType = FileSyncEventType.Changed,
                RelativePath = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        private void OnFileRenamed(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File renamed: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProject = Project,
                EventType = FileSyncEventType.Renamed,
                RelativePath = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }
        #endregion

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    watcher.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
