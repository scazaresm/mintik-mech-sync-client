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
    public class OpenVersionEventArgs : EventArgs
    {
        public LocalVersion Version { get; private set; }

        public OpenVersionEventArgs(LocalVersion version)
        {
            Version = version;
        }
    }

    public class OpenReviewEventArgs : EventArgs
    {
        public LocalReview Review { get; private set; }

        public OpenReviewEventArgs(LocalReview review)
        {
            Review = review;
        }
    }

    public class WorkspaceTreeView
    {
        public event EventHandler<OpenVersionEventArgs> OpenVersion;
        public event EventHandler<OpenReviewEventArgs> OpenReview;

        private readonly IMechSyncServiceClient serviceClient;
        private readonly string workspaceDirectory;

        private TreeNode myWorkNode;
        private TreeNode myReviewsNode;
        private TreeNode myVersionReviewsNode;
        private TreeNode myAssemblyFileReviewsNode;
        private TreeNode myDrawingFileReviewsNode;

        private Dictionary<string, Project> projectCache = new Dictionary<string, Project>();
        private Dictionary<string, Version> versionCache = new Dictionary<string, Version>();

        public TreeView AttachedTreeView { get; private set; }

        public WorkspaceTreeView(IMechSyncServiceClient serviceClient, string workspaceDirectory)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            this.workspaceDirectory = workspaceDirectory ?? throw new ArgumentNullException(nameof(workspaceDirectory));
            AttachTreeView(new TreeView());
        }

        public void AttachTreeView(TreeView treeView)
        {
            AttachedTreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));

            // create all nodes 
            myWorkNode = new TreeNode("My Work");
            myWorkNode.ImageIndex = 0;
            myWorkNode.SelectedImageIndex = 0;

            myReviewsNode = new TreeNode("My Reviews");

            myVersionReviewsNode = new TreeNode("Design");
            myAssemblyFileReviewsNode = new TreeNode("3D Files");
            myDrawingFileReviewsNode = new TreeNode("2D Files");

            // link related nodes
            myReviewsNode.Nodes.Add(myVersionReviewsNode);
            myReviewsNode.Nodes.Add(myAssemblyFileReviewsNode);
            myReviewsNode.Nodes.Add(myDrawingFileReviewsNode);

            // add root nodes to the new attached TreeView
            AttachedTreeView.Nodes.Clear();
            AttachedTreeView.Nodes.Add(myWorkNode);
            //AttachedTreeView.Nodes.Add(myReviewsNode);

            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
        }

        public async Task Refresh()
        {
            await FetchMyWorkAsync();
            //await FetchMyReviewsAsync();
        }

        private void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is LocalVersion version)
            {
                OpenVersion?.Invoke(this, new OpenVersionEventArgs(version));
                return;
            }

            if (e.Node.Tag is LocalReview review)
            {
                OpenReview?.Invoke(this, new OpenReviewEventArgs(review));
                return;
            }
        }

        private async Task FetchMyWorkAsync()
        {
            var myWork = await serviceClient.GetMyWorkAsync();

            // re-populate version-related nodes
            myWorkNode.Nodes.Clear();
            foreach(var remoteVersion in myWork)
            {
                var remoteProject = await GetProject(remoteVersion.ProjectId);
                var localVersion = new LocalVersion(remoteVersion, remoteProject, workspaceDirectory);
                var versionNode = new TreeNode(localVersion.ToString());
                versionNode.Tag = localVersion;
                versionNode.ImageIndex = 1;
                versionNode.SelectedImageIndex = 1;
                myWorkNode.Nodes.Add(versionNode);
            }
        }

        private async Task FetchMyReviewsAsync()
        {
            var myReviews = await serviceClient.GetMyReviewsAsync();

            // re-populate review-related nodes
            myVersionReviewsNode.Nodes.Clear();
            myAssemblyFileReviewsNode.Nodes.Clear();
            myDrawingFileReviewsNode.Nodes.Clear();

            foreach (var review in myReviews)
            {
                var remoteVersion = await GetVersion(review.VersionId);
                var remoteProject = await GetProject(remoteVersion.ProjectId);
                var localReview = new LocalReview(remoteVersion, remoteProject, review);
                var reviewNode = new TreeNode(localReview.ToString());
                reviewNode.Tag = localReview;

                switch (review.TargetType)
                {
                    case "Version": myVersionReviewsNode.Nodes.Add(reviewNode); break;
                    case "AssemblyFile": myAssemblyFileReviewsNode.Nodes.Add(reviewNode); break;
                    case "DrawingFile": myDrawingFileReviewsNode.Nodes.Add(reviewNode); break;
                    default: break;
                }
            }
        }

        private async Task<Project> GetProject(string projectId)
        {
            if (!projectCache.ContainsKey(projectId))
            {
                var project = await serviceClient.GetProjectAsync(projectId);
                projectCache.Add(projectId, project);
            }
            return projectCache[projectId];
        }

        private async Task<Version> GetVersion(string versionId)
        {
            if (!versionCache.ContainsKey(versionId))
            {
                var version = await serviceClient.GetVersionAsync(versionId);
                versionCache.Add(versionId, version);
            }
            return versionCache[versionId];
        }
    }
}
