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

namespace MechanicalSyncApp.UI
{
    public class OpenVersionEventArgs : EventArgs
    {
        public OngoingVersion Version { get; private set; }

        public OpenVersionEventArgs(OngoingVersion version)
        {
            Version = version;
        }
    }

    public class WorkspaceTreeView
    {
        public event EventHandler<OpenVersionEventArgs> OpenVersion;


        private readonly IMechSyncServiceClient serviceClient;
        private readonly string workspaceDirectory;

        private TreeNode myWorkNode;

        private Dictionary<string, Project> projectCache = new Dictionary<string, Project>();

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
            myWorkNode = new TreeNode("My Work");

            AttachedTreeView.Nodes.Add(myWorkNode);
            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
        }

        public void Refresh()
        {
            _ = FetchMyWorkAsync();
        }

        private void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is OngoingVersion version)
            {
                OpenVersion?.Invoke(this, new OpenVersionEventArgs(version));
            }
        }

        private async Task FetchMyWorkAsync()
        {
            var response = await serviceClient.GetMyOngoingVersionsAsync();

            myWorkNode.Nodes.Clear();
            foreach(var remoteVersion in response.MyOngoingVersions)
            {
                if(!projectCache.ContainsKey(remoteVersion.ProjectId))
                {
                    var project = await serviceClient.GetProjectAsync(remoteVersion.ProjectId);
                    projectCache.Add(remoteVersion.ProjectId, project);
                }

                var remoteProject = projectCache[remoteVersion.ProjectId];

                var localVersion = new OngoingVersion(remoteVersion, remoteProject, workspaceDirectory);
                var versionNode = new TreeNode(localVersion.ToString());
                versionNode.Tag = localVersion;
                myWorkNode.Nodes.Add(versionNode);
            }
        }
    }
}
