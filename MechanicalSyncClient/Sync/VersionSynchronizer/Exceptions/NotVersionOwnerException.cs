using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions
{
    public class NotVersionOwnerException : Exception
    {
        public NotVersionOwnerException()
        {
        }

        public NotVersionOwnerException(string message)
            : base(message)
        {
        }

        public NotVersionOwnerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
