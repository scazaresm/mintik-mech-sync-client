﻿using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class ConcurrentLocalFileIndexer
    {
        private const int THREADS_COUNT = 3;

        private readonly string directoryPath;
        private readonly string fileExtensionFilter;

        private ConcurrentQueue<string> fileQueue = new ConcurrentQueue<string>();

        public ConcurrentDictionary<string, FileMetadata> FileIndex { get; private set; } = new ConcurrentDictionary<string, FileMetadata>();

        public event EventHandler<int> ProgressChanged;

        public ConcurrentLocalFileIndexer(string directoryPath, string fileExtensionFilter)
        {
            this.directoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
            this.fileExtensionFilter = fileExtensionFilter ?? throw new ArgumentNullException(nameof(fileExtensionFilter));
        }

        public async Task IndexAsync()
        {
            int totalFileCount = 0;
            int completedFileCount = 0;

            foreach (string filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(filePath);

                // ommit lock files
                if (fileName.StartsWith("~$"))
                    continue;

                // ommit files not matching allowed extensions
                string fileExtension = $"*{Path.GetExtension(filePath)}";
                if (!fileExtensionFilter.ToLower().Contains(fileExtension.ToLower()))
                    continue;

                fileQueue.Enqueue(filePath);
                totalFileCount++;
            }

            var tasks = new Task[THREADS_COUNT];

            FileIndex.Clear();
            for (int i = 0; i < THREADS_COUNT; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    string filePath;
                    while (fileQueue.TryDequeue(out filePath))
                    {
                        var fileChecksum = await new Sha256ChecksumCalculator().CalculateChecksumAsync(filePath);

                        string relativeFilePath = filePath.Replace(directoryPath + Path.DirectorySeparatorChar, "");
                        relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

                        var metadata = new FileMetadata()
                        {
                            FileChecksum = fileChecksum,
                            RelativeFilePath = relativeFilePath
                        };
                        if (!FileIndex.ContainsKey(filePath))
                        {
                            FileIndex.TryAdd(relativeFilePath, metadata);
                        }

                        Interlocked.Increment(ref completedFileCount);
                        int progressPercentage = (int)((double)completedFileCount / totalFileCount * 100);
                        ProgressChanged?.Invoke(this, progressPercentage);
                    }
                    return Task.CompletedTask;
                });
            }
            await Task.WhenAll(tasks);
        }

    }
}
