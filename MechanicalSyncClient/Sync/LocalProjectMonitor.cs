using MechanicalSyncClient.Core;
using MechanicalSyncClient.Core.Domain;
using MechanicalSyncClient.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Sync
{
    class LocalProjectMonitor : ILocalProjectMonitor
    {
        private readonly List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private bool disposedValue;

        public LocalProject Project { get; }
        public string FileFilter { get; }

        private readonly object EventQueueLock = new object();
        public Queue<FileSyncEvent> EventQueue { get; }
        public bool IsProcessingQueuedEvents { get; private set; }

        public LocalProjectMonitor(LocalProject project, string fileFilter)
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
            EventQueue = new Queue<FileSyncEvent>();

            InitializeFileSystemWatchers();
        }

        public void InitializeFileSystemWatchers()
        {
            foreach(string filter in FileFilter.Split('|'))
            {
                var watcher = new FileSystemWatcher()
                {
                    Path = Project.FullPath,
                    Filter = filter.Trim()
                };
                watcher.Created += new FileSystemEventHandler(OnFileCreated);
                watcher.Deleted += new FileSystemEventHandler(OnFileDeleted);
                watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
                watcher.Changed += new FileSystemEventHandler(OnFileChanged);

                watcher.IncludeSubdirectories = true;
                watcher.EnableRaisingEvents = true;

                watchers.Add(watcher);
            }
        }

        private void DisposeFileSystemWatchers()
        {
            foreach (FileSystemWatcher watcher in watchers)
                watcher.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeFileSystemWatchers();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void ProcessQueuedEvents()
        {
            IsProcessingQueuedEvents = true;
            lock (EventQueueLock)
            {
                while (EventQueue.Count > 0)
                {
                    try
                    {
                        _ = DB.Async.InsertAsync(EventQueue.Dequeue());
                    }
                    catch(Exception ex)
                    {
                        // TODO: log exception
                    }
                }
            }
            IsProcessingQueuedEvents = false;
        }

        private void EnqueueEvent(FileSyncEvent e)
        {
            // skip lock files (used by apps to indicate a given file is in use)
            if (Path.GetFileName(e.FullPath).Trim().StartsWith("~$"))
                return;

            lock (EventQueueLock)
            {
                EventQueue.Enqueue(e);
            }
            if (!IsProcessingQueuedEvents)
                ProcessQueuedEvents();
        }

        public void OnFileCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File created: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProjectId = Project.LocalId,
                LocalProject = Project,
                EventType = FileSyncEventType.Created,
                Name = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        public void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File deleted: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProjectId = Project.LocalId,
                LocalProject = Project,
                EventType = FileSyncEventType.Deleted,
                Name = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        public void OnFileChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File changed: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProjectId = Project.LocalId,
                LocalProject = Project,
                EventType = FileSyncEventType.Changed,
                Name = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }

        public void OnFileRenamed(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File renamed: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProjectId = Project.LocalId,
                LocalProject = Project,
                EventType = FileSyncEventType.Renamed,
                Name = e.Name,
                FullPath = e.FullPath,
                RaiseDateTime = DateTime.Now
            });
        }
    }
}
