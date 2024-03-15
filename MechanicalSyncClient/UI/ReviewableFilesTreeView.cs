using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class OpenReviewTargetEventArgs : EventArgs
    {
        public LocalReview Review { get; private set; }

        public ReviewTarget ReviewTarget { get; private set; }

        public OpenReviewTargetEventArgs(LocalReview review, ReviewTarget reviewTarget)
        {
            Review = review ?? throw new ArgumentNullException(nameof(review));
            ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
        }
    }

    public abstract class ReviewableFilesTreeView
    {
        public event EventHandler<OpenReviewTargetEventArgs> OnOpenReviewTarget;

        protected readonly IMechSyncServiceClient serviceClient;
        protected readonly LocalReview review;
        protected readonly ILogger logger;

        public TreeView AttachedTreeView { get; private set; }

        protected TreeNode pendingNode;
        protected TreeNode reviewingNode;
        protected TreeNode reviewedNode;
        protected TreeNode fixedNode;

        protected Dictionary<string, ReviewTarget> reviewTargetLookup;

        public ReviewableFilesTreeView(IMechSyncServiceClient serviceClient, LocalReview review, ILogger logger)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
            this.logger = logger;
            AttachTreeView(new TreeView());
        }

        public void AttachTreeView(TreeView treeView)
        {
            AttachedTreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            pendingNode = new TreeNode("Pending");
            pendingNode.ImageIndex = 0;

            reviewingNode = new TreeNode("Reviewing");
            reviewingNode.ImageIndex = 0;

            reviewedNode = new TreeNode("Reviewed");
            reviewedNode.ImageIndex = 0;

            fixedNode = new TreeNode("Fixed");
            fixedNode.ImageIndex = 0;

            AttachedTreeView.Nodes.AddRange(new TreeNode[] {
                pendingNode, reviewingNode, reviewedNode, fixedNode
            });
            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
        }

        public abstract Task<List<FileMetadata>> FetchReviewableFileMetadata();

        public async Task RefreshAsync()
        {
            await SyncReviewTargets();

            // clear all nodes
            pendingNode.Nodes.Clear();
            reviewingNode.Nodes.Clear();
            reviewedNode.Nodes.Clear();
            fixedNode.Nodes.Clear();

            var reviewedIconIndexes = new Dictionary<string, int>()
            {
                { "Approved", 2 },
                { "Rejected", 3 },
                { "Fixed", 4 }
            };

            var allReviewableFileMetadata = await FetchReviewableFileMetadata();

            foreach (var reviewableFileMetadata in allReviewableFileMetadata)
            {
                if (!reviewTargetLookup.ContainsKey(reviewableFileMetadata.Id)) continue;

                var reviewTarget = reviewTargetLookup[reviewableFileMetadata.Id];
                var reviewableFileName = Path.GetFileName(reviewableFileMetadata.FullFilePath);
                TreeNode newTreeNode = null;

                switch (reviewTarget.Status)
                {
                    case "Pending":
                        newTreeNode = pendingNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Reviewing":
                        newTreeNode = reviewingNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Rejected":
                    case "Approved":
                        newTreeNode = reviewedNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = reviewedIconIndexes[reviewTarget.Status];
                        newTreeNode.SelectedImageIndex = newTreeNode.ImageIndex;
                        break;

                    case "Fixed":
                        newTreeNode = fixedNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = reviewedIconIndexes[reviewTarget.Status];
                        newTreeNode.SelectedImageIndex = newTreeNode.ImageIndex;
                        break;
                }

                if (newTreeNode != null)
                {
                    newTreeNode.Tag = reviewTarget;
                }
            }
            AttachedTreeView.ExpandAll();
        }

        protected async Task SyncReviewTargets()
        {
            review.RemoteReview = await serviceClient.SyncReviewTargetsAsync(review.RemoteReview.Id);
            reviewTargetLookup = new Dictionary<string, ReviewTarget>();
            foreach (var reviewTarget in review.RemoteReview.Targets)
            {
                reviewTargetLookup.Add(reviewTarget.TargetId, reviewTarget);
            }
        }

        private void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ReviewTarget target)
            {
                OnOpenReviewTarget?.Invoke(this, new OpenReviewTargetEventArgs(review, target));
            }
        }
    }
}
