using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface IModelPropertiesRetriever
    {
        Dictionary<string, string> GetAllCustomPropertyValues(string filePath);
        Task<Dictionary<string, string>> GetAllCustomPropertyValuesAsync(string filePath);
    }
}