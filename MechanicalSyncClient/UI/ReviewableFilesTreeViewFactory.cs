using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.UI
{
    public class ReviewableFilesTreeViewFactory
    {
        // Review target type vs. corresponding ReviewableFilesTreeView implementation instance
        private readonly Dictionary<string, ReviewableFilesTreeView> _instances;

        public ReviewableFilesTreeViewFactory(IMechSyncServiceClient mechSyncService, LocalReview review, ILogger logger)
        {
            _instances = new Dictionary<string, ReviewableFilesTreeView>()
            {
                {  ReviewTargetType.AssemblyFile.ToString(), new ReviewableAssembliesTreeView(mechSyncService, review, logger) },
                {  ReviewTargetType.DrawingFile.ToString(), new ReviewableDrawingsTreeView(mechSyncService, review, logger) }
            };
        }

        public ReviewableFilesTreeView GetTreeView(Review review)
        {
            if (!_instances.ContainsKey(review.TargetType))
                throw new InvalidOperationException($"Could not create ReviewableFilesTreeView instance, review target type {review.TargetType} is not supported.");

            return _instances[review.TargetType];
        }
    }
}
