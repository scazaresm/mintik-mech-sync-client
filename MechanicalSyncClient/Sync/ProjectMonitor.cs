﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Database;
using MechanicalSyncApp.Database.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace MechanicalSyncApp.Sync
{
    class ProjectMonitor : IProjectMonitor
    {
        private FileSystemWatcher watcher;
        private bool disposedValue;

        public LocalProject Project { get; }
        public string FileFilter { get; }

        private readonly object EventQueueLock = new object();
        public Queue<FileSyncEvent> EventQueue { get; }

        public bool IsMonitoring { get; private set; }

        public ProjectMonitor(LocalProject project, string fileFilter)
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

            InitializeFileSystemWatcher();
        }

        public bool IsEventQueueEmpty()
        {
            lock (EventQueueLock)
            {
                return EventQueue.Count == 0;
            }
        }

        public FileSyncEvent PeekNextEvent()
        {
            lock (EventQueueLock)
            {
                if (EventQueue.Count == 0)
                    return null;

                return EventQueue.Peek();
            }
        }

        public FileSyncEvent DequeueEvent()
        {
            lock (EventQueueLock)
            {
                if (EventQueue.Count == 0)
                    return null;

                return EventQueue.Dequeue();
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
                Path = Project.FullPath,
                IncludeSubdirectories = true
            };
            watcher.Created += new FileSystemEventHandler(OnFileCreated);
            watcher.Deleted += new FileSystemEventHandler(OnFileDeleted);
            watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
            watcher.Changed += new FileSystemEventHandler(OnFileChanged);
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
        }

        #region FileSystemWatcher event callbacks
        private void OnFileCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File created: {e.Name}");
            EnqueueEvent(new FileSyncEvent
            {
                LocalProjectId = Project.LocalId,
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
                LocalProjectId = Project.LocalId,
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
                LocalProjectId = Project.LocalId,
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
                LocalProjectId = Project.LocalId,
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
