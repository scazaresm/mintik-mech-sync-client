using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.UI
{
    public class SelectedVersionDetails
    {
        public Version Version { get; set; }
        public Project Project { get; set; }
        public UserDetails OwnerDetails { get; set; }

        public override string ToString()
        {
            return $"{Project.FolderName} V{Version.Major}";
        }
    }
}
