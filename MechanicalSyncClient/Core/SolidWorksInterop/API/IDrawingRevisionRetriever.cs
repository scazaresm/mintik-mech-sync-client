using SolidWorks.Interop.sldworks;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface IDrawingRevisionRetriever
    {
        string GetRevision(string drawingPath);
        Task<string> GetRevisionAsync(string drawingPath);
    }
}