using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public interface IModelPropertiesRetriever
    {
        Dictionary<string, string> GetAllCustomProperties(string filePath);
        Task<Dictionary<string, string>> GetAllCustomPropertiesAsync(string filePath);
    }
}