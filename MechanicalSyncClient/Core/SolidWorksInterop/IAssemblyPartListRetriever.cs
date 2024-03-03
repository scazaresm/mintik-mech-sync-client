using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public interface IAssemblyPartListRetriever
    {
        HashSet<string> ExtractDistinctPartList(string assemblyFilePath);
        Task<HashSet<string>> ExtractDistinctPartListAsync(string assemblyFilePath);
    }
}