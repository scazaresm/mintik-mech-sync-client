using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions
{
    public class VersionFolderAlreadyExistsException : Exception
    {
        public VersionFolderAlreadyExistsException()
        {
        }

        public VersionFolderAlreadyExistsException(string message)
            : base(message)
        {
        }

        public VersionFolderAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
