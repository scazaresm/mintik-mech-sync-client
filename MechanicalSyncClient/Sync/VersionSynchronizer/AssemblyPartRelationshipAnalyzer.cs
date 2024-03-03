using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class AssemblyPartRelationshipAnalyzer
    {
        private readonly ISolidWorksStarter solidWorksStarter;
        private readonly IVersionSynchronizer synchronizer;
        private readonly IReviewableFileMetadataFetcher reviewableFetcher;
        private readonly CancellationTokenSource cts;
        private readonly ILogger logger;

        public DesignFilesAnalysisDialog Dialog { get; set; }

        public AssemblyPartRelationshipAnalyzer(
            ISolidWorksStarter solidWorksStarter, 
            IVersionSynchronizer synchronizer, 
            IReviewableFileMetadataFetcher reviewableFetcher,
            CancellationTokenSource cts,
            ILogger logger
            )
        {
            this.solidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.reviewableFetcher = reviewableFetcher ?? throw new ArgumentNullException(nameof(reviewableFetcher));
            this.cts = cts ?? throw new ArgumentNullException(nameof(cts));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Dictionary<string, HashSet<string>>> AnalyzeAsync()
        {
            var partsInAssemblyLookup = new Dictionary<string, HashSet<string>>();

            Dialog?.SetStatus("Identifying part-assembly relationships");

            var assemblyFiles = await reviewableFetcher.FetchReviewableAssembliesAsync();
            var partListRetriever = new AssemblyPartListRetriever(solidWorksStarter, logger);

            int analyzedCount = 0;

            foreach (var assembly in assemblyFiles)
            {
                cts.Token.ThrowIfCancellationRequested();

                Dialog?.SetDetails($"Analyzing {Path.GetFileName(assembly.RelativeFilePath)}...");

                var fullAssemblyPath = Path.Combine(
                    synchronizer.Version.LocalDirectory,
                    assembly.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar)
                );

                var partsInAssembly = await partListRetriever.ExtractDistinctPartListAsync(fullAssemblyPath);

                foreach (var part in partsInAssembly)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    if (!partsInAssemblyLookup.ContainsKey(part))
                        partsInAssemblyLookup.Add(part, new HashSet<string> { fullAssemblyPath });
                    else
                        partsInAssemblyLookup[part].Add(fullAssemblyPath);
                }
                analyzedCount++;
                var progress = (int)(((double)analyzedCount / assemblyFiles.Count) * 100.0);
                Dialog?.SetProgress(progress);
            }
            return partsInAssemblyLookup;
        }
    }
}
