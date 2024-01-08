using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Exceptions
{
    public class ProjectFolderAlreadyExistsException : Exception
    {
        public ProjectFolderAlreadyExistsException()
        {
        }

        public ProjectFolderAlreadyExistsException(string message)
            : base(message)
        {
        }

        public ProjectFolderAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
