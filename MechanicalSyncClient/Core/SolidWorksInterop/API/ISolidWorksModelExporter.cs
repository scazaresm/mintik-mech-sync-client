using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface ISolidWorksModelExporter
    {
        void ExportModel(string sourceFile, string outputFile);
        void ExportModel(string sourceFile, string[] outputFiles);
        Task ExportModelAsync(string sourceFile, string outputFile);
        Task ExportModelAsync(string sourceFile, string[] outputFiles);
    }
}