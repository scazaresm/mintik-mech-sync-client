using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public interface IChecksumCalculator
    {
        string CalculateChecksum(string filePath);
        Task<string> CalculateChecksumAsync(string filePath);
    }
}
