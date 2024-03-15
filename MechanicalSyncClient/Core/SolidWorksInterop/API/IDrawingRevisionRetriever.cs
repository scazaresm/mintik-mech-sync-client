using SolidWorks.Interop.sldworks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface IDrawingRevisionRetriever
    {
        string GetRevision(string drawingPath);
    }
}