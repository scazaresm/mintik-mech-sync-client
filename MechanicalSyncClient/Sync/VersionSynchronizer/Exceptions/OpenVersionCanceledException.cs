using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions
{
    public class OpenVersionCanceledException : Exception
    {
        public OpenVersionCanceledException()
        {
        }

        public OpenVersionCanceledException(string message)
            : base(message)
        {
        }

        public OpenVersionCanceledException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
