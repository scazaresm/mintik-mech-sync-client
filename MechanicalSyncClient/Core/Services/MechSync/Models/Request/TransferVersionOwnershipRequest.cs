using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class TransferVersionOwnershipRequest
    {
        public string VersionId { get; set; }

        public string Username { get; set; }

    }
}
