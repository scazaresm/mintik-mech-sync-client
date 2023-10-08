using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class FileMetadataChecker : IFileMetadataChecker
    {
        private readonly LocalProject localProject;
        private readonly IProjectChangeMonitor changeMonitor;
        private readonly IChecksumCalculator checksumCalculator;

        private readonly object checkQueueLock = new object();
        public Queue<FileMetadata> CheckQueue { get; }

        public FileMetadataChecker(LocalProject localProject, IProjectChangeMonitor changeMonitor, IChecksumCalculator checksumCalculator)
        {
            this.localProject = localProject ?? throw new ArgumentNullException(nameof(localProject));
            this.changeMonitor = changeMonitor ?? throw new ArgumentNullException(nameof(changeMonitor));
            this.checksumCalculator = checksumCalculator ?? throw new ArgumentNullException(nameof(checksumCalculator));
        }

        public async Task CheckAsync(FileMetadata metadata)
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            var fullFilePath = Path.Combine(localProject.LocalDirectory, metadata.RelativeFilePath);

            if (!File.Exists(fullFilePath))
                return;

            var actualFileChecksum = await checksumCalculator.CalculateChecksumAsync(fullFilePath);

            if (metadata.FileChecksum != actualFileChecksum)
            {
                changeMonitor.EnqueueEvent(new FileSyncEvent()
                {
                    LocalProject = localProject,
                    EventType = FileSyncEventType.Changed,
                    FullPath = fullFilePath,
                    RelativePath = metadata.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued,
                });
            }
        }
    }
}
