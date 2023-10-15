using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    public class VersionExplorerControl
    {
        public event EventHandler<OpenVersionEventArgs> OpenVersion;

        private readonly IMechSyncServiceClient serviceClient;

        private TreeNode myWorkspaceNode;
        private TreeNode othersWorkspaceNode;

        private Dictionary<string, Project> projectCache = new Dictionary<string, Project>();

        public TreeView AttachedTreeView { get; private set; }

        public VersionExplorerControl(IMechSyncServiceClient serviceClient)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            AttachTreeView(new TreeView());
        }

        public void AttachTreeView(TreeView treeView)
        {
            AttachedTreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            myWorkspaceNode = new TreeNode("My workspace");

            AttachedTreeView.Nodes.Add(myWorkspaceNode);

            AttachedTreeView.NodeMouseDoubleClick += AttachedTreeView_NodeMouseDoubleClick;
        }

        public void Refresh()
        {
            _ = FetchMyWorkspaceAsync();
        }


        private void AttachedTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Tag is LocalVersion)
            {
                var version = (LocalVersion)e.Node.Tag;
                OnOpenVersion(new OpenVersionEventArgs(version));
            }
        }

        protected void OnOpenVersion(OpenVersionEventArgs e)
        {
            OpenVersion?.Invoke(this, e);
        }

        private async Task FetchMyWorkspaceAsync()
        {
            var response = await serviceClient.GetMyOngoingVersionsAsync();

            myWorkspaceNode.Nodes.Clear();
            foreach(var remoteVersion in response.MyOngoingVersions)
            {
                if(!projectCache.ContainsKey(remoteVersion.ProjectId))
                {
                    var project = await serviceClient.GetProjectAsync(remoteVersion.ProjectId);
                    projectCache.Add(remoteVersion.ProjectId, project);
                }
                var localVersion = new LocalVersion(
                    remoteVersion, 
                    projectCache[remoteVersion.ProjectId],
                    @"C:\sync_demo"
                );
                var versionNode = new TreeNode(localVersion.ToString());
                versionNode.Tag = localVersion;
                myWorkspaceNode.Nodes.Add(versionNode);
            }
        }
    }
}
