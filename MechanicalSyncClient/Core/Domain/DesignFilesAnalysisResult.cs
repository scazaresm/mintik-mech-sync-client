using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public class DesignFilesAnalysisResult
    {
        public Exception ExceptionObject { get; set; }

        public Dictionary<string, HashSet<string>> PartsInAssemblyLookup { get; set; } = new Dictionary<string, HashSet<string>>();
    }
}
