using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.Domain
{
    public enum FileSyncEventState
    {
        Queued,
        Processing,
        Processed
    }
}
