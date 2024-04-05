using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.UI
{
    public class OpenDrawingForViewingEventArgs : EventArgs
    {
        public Review Review { get; private set; }
        public ReviewTarget ReviewTarget { get; private set; }

        public FileMetadata DrawingMetadata { get; set; }

        public OpenDrawingForViewingEventArgs(Review review, ReviewTarget reviewTarget, FileMetadata drawingMetadata)
        {
            Review = review ?? throw new ArgumentNullException(nameof(review));
            ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
            DrawingMetadata = drawingMetadata ?? throw new ArgumentNullException(nameof(drawingMetadata));
        }
    }

    public enum ReviewTargetType 
    {
        DrawingFile,
        AssemblyFile
    }

    public class DeliverableReviewsTreeView : IDisposable
    {
        public event EventHandler<OpenDrawingForViewingEventArgs> OpenReviewForViewing;

        public IMechSyncServiceClient MechSyncService { get; private set; }
        public IAuthenticationServiceClient AuthService { get; private set; }
        public LocalVersion Version { get; }
        public TreeView AttachedTreeView { get; private set; }

        private readonly Dictionary<string, FileMetadata> reviewTargetIndex = new Dictionary<string, FileMetadata>();
        private readonly ReviewTargetType reviewTargetType;

        public string[] ReviewedStatuses { get; } = new string[]
        {
           "Rejected", "Approved"
        };

        private TreeNode rootNode;
        private bool disposedValue;

        public DeliverableReviewsTreeView(IMechSyncServiceClient mechSyncService, 
                                          LocalVersion version,
                                          ReviewTargetType reviewTargetType
            )
        {
            MechSyncService = mechSyncService ?? throw new ArgumentNullException(nameof(mechSyncService));
            AuthService = MechSyncService.AuthenticationService;
            Version = version;
            this.reviewTargetType = reviewTargetType;
        }

        public void AttachTreeView(TreeView treeView)
        {
            AttachedTreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            rootNode = new TreeNode("Review Progress");

            AttachedTreeView.Nodes.Clear();
            AttachedTreeView.Nodes.Add(rootNode);
            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
          
        }

        public async Task Refresh()
        {
            var reviews = await MechSyncService.GetVersionReviewsAsync(Version.RemoteVersion.Id);

            AttachedTreeView.Nodes.Clear();
            foreach (var review in reviews)
            {
                // we are interested on the specified review targets only
                if (review.TargetType != reviewTargetType.ToString()) continue;

                // sync review targets to detect deleted files
                var syncedReview = await MechSyncService.SyncReviewTargetsAsync(review.Id);

                var reviewerDetails = await AuthService.GetUserDetailsAsync(syncedReview.ReviewerId);

                var reviewNode = new TreeNode(reviewerDetails.FullName)
                {
                    Tag = syncedReview,
                    ImageIndex = 1,
                    SelectedImageIndex = 1
                };

                await PopulateReviewTargets(reviewNode);

                AttachedTreeView.Nodes.Add(reviewNode);
            }
        }

        private async Task PopulateReviewTargets(TreeNode reviewNode)
        {
            var review = reviewNode.Tag as Review;

            reviewNode.Nodes.Clear();

            foreach (var reviewTarget in review.Targets)
            {
                var targetDetails = await GetReviewTargetDetailsAsync(reviewTarget.TargetId);

                var isTargetReviewed = ReviewedStatuses.Contains(reviewTarget.Status);

                if (!isTargetReviewed)
                    continue;

                var targetFileName = Path.GetFileName(targetDetails.FullFilePath);

                var reviewTargetNode = new TreeNode(targetFileName);
                reviewTargetNode.Tag = reviewTarget;

                switch (reviewTarget.Status)
                {
                    case "Approved":
                        reviewTargetNode.ImageIndex = 2;
                        reviewTargetNode.SelectedImageIndex = 2; 
                        break;
                    case "Rejected":
                        reviewTargetNode.ImageIndex = 3;
                        reviewTargetNode.SelectedImageIndex = 3; 
                        break;
                    case "Fixed":
                        reviewTargetNode.ImageIndex = 4;
                        reviewTargetNode.SelectedImageIndex = 4;
                        break;
                }
                reviewNode.Nodes.Add(reviewTargetNode);
            }
            reviewNode.ExpandAll();
        }

        private async void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ReviewTarget)
            {
                var review = e.Node.Parent.Tag as Review;
                var reviewTarget = e.Node.Tag as ReviewTarget;

                var reviewTargetMetadata = await GetReviewTargetDetailsAsync(reviewTarget.TargetId);

                OpenReviewForViewing?.Invoke(sender,
                    new OpenDrawingForViewingEventArgs(review, reviewTarget, reviewTargetMetadata)
                );
            }
        }

        private async Task<FileMetadata> GetReviewTargetDetailsAsync(string targetId)
        {
            if (!reviewTargetIndex.ContainsKey(targetId))
                reviewTargetIndex[targetId] = await MechSyncService.GetFileMetadataAsync(targetId);

            return reviewTargetIndex[targetId];
        }

        #region Dispose pattern

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    AttachedTreeView.NodeMouseDoubleClick -= AttachedTreeView_NodeMouseDoubleClick;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
