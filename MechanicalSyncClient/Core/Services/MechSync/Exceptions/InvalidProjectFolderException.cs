using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Exceptions
{
    public class InvalidProjectFolderException : Exception
    {
        public InvalidProjectFolderException()
        {
        }

        public InvalidProjectFolderException(string message)
            : base(message)
        {
        }

        public InvalidProjectFolderException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
