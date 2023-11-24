using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions
{
    public class VersionOwnershipNotAcknowledgedException : Exception
    {
        public VersionOwnershipNotAcknowledgedException()
        {
        }

        public VersionOwnershipNotAcknowledgedException(string message)
            : base(message)
        {
        }

        public VersionOwnershipNotAcknowledgedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
