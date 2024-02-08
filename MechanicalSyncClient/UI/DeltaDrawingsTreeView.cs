using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class OpenDrawingForReviewingEventArgs : EventArgs
    {
        public LocalReview Review { get; private set; }
        public ReviewTarget ReviewTarget { get; private set; }

        public OpenDrawingForReviewingEventArgs(LocalReview review, ReviewTarget reviewTarget)
        {
            Review = review ?? throw new ArgumentNullException(nameof(review));
            ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
        }
    }

    public class DeltaDrawingsTreeView
    {
        public event EventHandler<OpenDrawingForReviewingEventArgs> OnOpenDrawingForReviewing;

        private readonly IMechSyncServiceClient serviceClient;
        private readonly LocalReview review;

        public TreeView AttachedTreeView { get; private set; }

        private TreeNode pendingNode;
        private TreeNode reviewingNode;
        private TreeNode reviewedNode;

        private Dictionary<string, ReviewTarget> reviewTargetLookup;

        public DeltaDrawingsTreeView(IMechSyncServiceClient serviceClient, LocalReview review)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
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

            AttachedTreeView.Nodes.AddRange(new TreeNode[] { pendingNode, reviewingNode, reviewedNode });
            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
        }

        public async Task Refresh()
        {
            await FetchDrawingsAsync();
        }

        private void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ReviewTarget)
            {
                OnOpenDrawingForReviewing?.Invoke(this, new OpenDrawingForReviewingEventArgs(review, (ReviewTarget)e.Node.Tag));
            }
        }

        private async Task SyncReviewTargets()
        {
            review.RemoteReview = await serviceClient.SyncReviewTargetsAsync(review.RemoteReview.Id);
            reviewTargetLookup = new Dictionary<string, ReviewTarget>();
            foreach(var reviewTarget in review.RemoteReview.Targets) 
            {
                reviewTargetLookup.Add(reviewTarget.TargetId, reviewTarget);
            }
        }

        private async Task FetchDrawingsAsync()
        {
            await SyncReviewTargets();

            // delta file metadata means all the files being added or modified on this version
            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = review.RemoteVersion.Id
            });

            // we are only interested on drawing files
            var deltaDrawings = deltaFileMetadata.Where((m) => m.RelativeFilePath.ToLower().EndsWith(".slddrw")).ToList();

            // clear all nodes
            pendingNode.Nodes.Clear();
            reviewingNode.Nodes.Clear();   
            reviewedNode.Nodes.Clear();

            var reviewedIconIndexes = new Dictionary<string, int>()
            {       
                { "Approved", 2 },
                { "Rejected", 3 },
                { "Fixed", 4 }
            };


            foreach (var drawingMetadata in deltaDrawings)
            {
                if (!reviewTargetLookup.ContainsKey(drawingMetadata.Id)) continue;

                var reviewTarget = reviewTargetLookup[drawingMetadata.Id];
                var drawingFileName = Path.GetFileName(drawingMetadata.FullFilePath);
                TreeNode newTreeNode = null;

                switch (reviewTarget.Status)
                {
                    case "Pending":
                        newTreeNode = pendingNode.Nodes.Add(drawingFileName);
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Reviewing":
                        newTreeNode = reviewingNode.Nodes.Add(drawingFileName); 
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        break;

                    case "Rejected":
                    case "Approved":
                    case "Fixed":
                        newTreeNode = reviewedNode.Nodes.Add(drawingFileName);
                        newTreeNode.ImageIndex = reviewedIconIndexes[reviewTarget.Status];
                        newTreeNode.SelectedImageIndex = newTreeNode.ImageIndex;
                        break;
                }

                if (newTreeNode != null)
                {
                    newTreeNode.Tag = reviewTarget;
                }
            }
        }

    }
}
