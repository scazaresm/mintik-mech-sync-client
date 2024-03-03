using SolidWorks.Interop.sldworks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public interface IDrawingRevisionRetriever
    {
        string GetRevision(string drawingPath);
    }
}