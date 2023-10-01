using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class ChecksumMismatchException : Exception
    {
        public ChecksumMismatchException()
        {
        }

        public ChecksumMismatchException(string message)
            : base(message)
        {
        }

        public ChecksumMismatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public interface IChecksumValidator
    {
        string CalculateFromFile(string filePath);
        void ValidateFileChecksum(string filePath, string expectedChecksum);
    }
}
