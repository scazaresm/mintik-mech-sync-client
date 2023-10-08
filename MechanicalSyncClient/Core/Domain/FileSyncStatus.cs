using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public enum FileSyncStatus
    {
        None,
        Created,
        Modified,
        Deleted,
        Synced
    }
}
