using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
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
        private const int THREADS_COUNT = 6;

        private readonly string directoryPath;
        private readonly string fileExtensionFilter;

        private int totalFileCount = 0;
        private int completedFileCount = 0;

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
            totalFileCount = 0;
            completedFileCount = 0;
            FileIndex.Clear();
            EnqueueLocalFiles();

            var tasks = new Task[THREADS_COUNT];

            for (int i = 0; i < THREADS_COUNT; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    string filePath;
                    while (fileQueue.TryDequeue(out filePath))
                    {
                        Log.Debug($"\tIndexing local file {filePath}");
                        await IndexFileAsync(filePath);

                        Interlocked.Increment(ref completedFileCount);
                        int progressPercentage = (int)((double)completedFileCount / totalFileCount * 100);
                        ProgressChanged?.Invoke(this, progressPercentage);
                    }
                    return Task.CompletedTask;
                });
            }
            await Task.WhenAll(tasks);
        }

        private async Task IndexFileAsync(string filePath)
        {
            var fileChecksum = await new Sha256FileChecksumCalculator().CalculateChecksumAsync(filePath);

            string relativeFilePath = filePath.Replace(directoryPath + Path.DirectorySeparatorChar, "");
            relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

            var metadata = new FileMetadata()
            {
                FileChecksum = fileChecksum,
                RelativeFilePath = relativeFilePath
            };
            FileIndex.TryAdd(relativeFilePath, metadata);
        }

        private void EnqueueLocalFiles()
        {
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
        }
    }
}
