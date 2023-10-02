using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IWorkspaceManager
    {
        string WorkspaceDirectory { get; }
        MechSyncServiceClient ServiceClient { get; }

        void Initialize();
        void DownloadRemoteProject(string remoteProjectId);
        void PublishLocalProject(LocalProject localProject);
        void CreateProjectVersion(string remoteProjectId);
    }
}
