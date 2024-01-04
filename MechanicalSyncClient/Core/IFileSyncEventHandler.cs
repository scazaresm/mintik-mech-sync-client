using MechanicalSyncApp.Core.Domain;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IFileSyncEventHandler
    {
        Task HandleAsync(FileSyncEvent fileSyncEvent);
    }
}
