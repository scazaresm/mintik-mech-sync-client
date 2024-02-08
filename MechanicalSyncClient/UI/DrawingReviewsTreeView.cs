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
        public LocalReview Review { get; private set; }
        public ReviewTarget ReviewTarget { get; private set; }

        public OpenDrawingForViewingEventArgs(LocalReview review, ReviewTarget reviewTarget)
        {
            Review = review ?? throw new ArgumentNullException(nameof(review));
            ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
        }
    }

    public class DrawingReviewsTreeView : IDisposable
    {
        public IMechSyncServiceClient MechSyncService { get; private set; }
        public IAuthenticationServiceClient AuthService { get; private set; }
        public LocalVersion Version { get; }
        public TreeView AttachedTreeView { get; private set; }

        public string[] ReviewedStatuses { get; } = new string[]
        {
           "Rejected", "Approved"
        };

        private TreeNode rootNode;
        private bool disposedValue;

        public DrawingReviewsTreeView(IMechSyncServiceClient mechSyncService,
                                      IAuthenticationServiceClient authService, 
                                      LocalVersion version
            )
        {
            MechSyncService = mechSyncService ?? throw new ArgumentNullException(nameof(mechSyncService));
            AuthService = authService ?? throw new ArgumentNullException(nameof(authService));
            Version = version;
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
            var reviews = await MechSyncService.GetVersionReviews(Version.RemoteVersion.Id);

            rootNode.Nodes.Clear();
            foreach (var review in reviews)
            {
                var reviewerDetails = await AuthService.GetUserDetailsAsync(review.ReviewerId);

                var reviewNode = new TreeNode(reviewerDetails.FullName);
                reviewNode.Tag = review;
                rootNode.Nodes.Add(reviewNode);
            }
        }

        private async void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Review)
                await PopulateReviewTargets(e.Node);

        }

        private async Task PopulateReviewTargets(TreeNode reviewNode)
        {
            var review = reviewNode.Tag as Review;

            reviewNode.Nodes.Clear();

            foreach (var reviewTarget in review.Targets)
            {
                var targetDetails = await MechSyncService.GetFileMetadataAsync(reviewTarget.TargetId);

                var isDrawing = targetDetails.FullFilePath.ToLower().EndsWith(".slddrw");
                var isDrawingReviewed = ReviewedStatuses.Contains(reviewTarget.Status);

                if (!isDrawing || !isDrawingReviewed)
                    continue;

                var targetFileName = Path.GetFileName(targetDetails.FullFilePath);

                var reviewTargetNode = new TreeNode(targetFileName);
                reviewNode.Nodes.Add(reviewTargetNode);
            }
            reviewNode.ExpandAll();
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
