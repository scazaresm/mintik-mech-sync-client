using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    public class ReviewRightClickEventArgs : EventArgs
    {
        public LocalReview Review { get; private set; }

        public ReviewTarget ReviewTarget { get; private set; }
        public TreeNode Node { get; }
        public Point Location { get; }

        public ReviewRightClickEventArgs(LocalReview review, ReviewTarget reviewTarget, TreeNode node, Point location)
        {
            Review = review ?? throw new ArgumentNullException(nameof(review));
            ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Location = location;
        }
    }

    public abstract class ReviewableFilesTreeView
    {
        public event EventHandler<OpenReviewTargetEventArgs> OnOpenReviewTarget;
        public event EventHandler<ReviewRightClickEventArgs> OnReviewRightClick;

        private const string REVIEWED_FOLDER_NAME = "Reviewed";
        private const string PENDING_FOLDER_NAME = "Pending";
        private const string REVIEWING_FOLDER_NAME = "Reviewing";
        private const string FIXED_FOLDER_NAME = "Fixed";

        protected readonly IMechSyncServiceClient serviceClient;
        protected readonly LocalReview review;
        protected readonly ILogger logger;

        public TreeView AttachedTreeView { get; private set; }

        public TreeNode PendingNode { get; private set; }
        public TreeNode ReviewingNode { get; private set; }
        public TreeNode ReviewedNode { get; private set; }
        public TreeNode FixedNode { get; private set; }

        protected Dictionary<string, ReviewTarget> reviewTargetLookup;

        public ReviewableFilesTreeView(IMechSyncServiceClient serviceClient, LocalReview review,  ILogger logger)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
            this.logger = logger;
            AttachTreeView(new TreeView());
        }

        public void AttachTreeView(TreeView treeView)
        {
            AttachedTreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            PendingNode = new TreeNode(PENDING_FOLDER_NAME);
            PendingNode.ImageIndex = 0;

            ReviewingNode = new TreeNode(REVIEWING_FOLDER_NAME);
            ReviewingNode.ImageIndex = 0;

            ReviewedNode = new TreeNode(REVIEWED_FOLDER_NAME);
            ReviewedNode.ImageIndex = 0;

            FixedNode = new TreeNode(FIXED_FOLDER_NAME);
            FixedNode.ImageIndex = 0;

            AttachedTreeView.Nodes.AddRange(new TreeNode[] {
                PendingNode, ReviewingNode, ReviewedNode, FixedNode
            });
            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
            AttachedTreeView.MouseDown += AttachedTreeView_MouseDown;
        }

        public abstract Task<List<FileMetadata>> FetchReviewableFileMetadata();

        public async Task RefreshAsync()
        {
            await SyncReviewTargets();

            // clear all nodes
            PendingNode.Nodes.Clear();
            ReviewingNode.Nodes.Clear();
            ReviewedNode.Nodes.Clear();
            FixedNode.Nodes.Clear();

            var reviewedIconIndexes = new Dictionary<string, int>()
            {
                { "Approved", 2 },
                { "Rejected", 3 },
                { "Fixed", 4 }
            };

            var allReviewableFileMetadata = await FetchReviewableFileMetadata();
            allReviewableFileMetadata.Sort((a, b) => a.RelativeFilePath.CompareTo(b.RelativeFilePath));

            foreach (var reviewableFileMetadata in allReviewableFileMetadata)
            {
                if (!reviewTargetLookup.ContainsKey(reviewableFileMetadata.Id)) continue;

                var reviewTarget = reviewTargetLookup[reviewableFileMetadata.Id];
                var reviewableFileName = Path.GetFileName(reviewableFileMetadata.FullFilePath);
                TreeNode newTreeNode = null;

                switch (reviewTarget.Status)
                {
                    case "Pending":
                        newTreeNode = PendingNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Reviewing":
                        newTreeNode = ReviewingNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Rejected":
                    case "Approved":
                        newTreeNode = ReviewedNode.Nodes.Add(reviewableFileName);
                        newTreeNode.ImageIndex = reviewedIconIndexes[reviewTarget.Status];
                        newTreeNode.SelectedImageIndex = newTreeNode.ImageIndex;
                        break;

                    case "Fixed":
                        newTreeNode = FixedNode.Nodes.Add(reviewableFileName);
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

        private void AttachedTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the node at the mouse position
                var clickedNode = AttachedTreeView.GetNodeAt(e.X, e.Y);

                if ( IsReviewNode(clickedNode) )
                {
                    OnReviewRightClick?.Invoke(
                        this, 
                        new ReviewRightClickEventArgs(review, (clickedNode.Tag as ReviewTarget), 
                        clickedNode,
                        e.Location
                    ));
                }
            }
        }

        private bool IsReviewNode(TreeNode node)
        {
            var parentNode = node.Parent;
            return node != null && parentNode != null && parentNode.Text == REVIEWED_FOLDER_NAME && node.Tag is ReviewTarget;
        }
    }
}
