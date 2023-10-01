using MechanicalSyncApp.Database.Domain;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IFileSyncEventHandler
    {
        Task HandleAsync(FileSyncEvent fileSyncEvent);
        Task HandleAsync(FileSyncEvent fileSyncEvent, int retryLimit);
    }
}
