using MechanicalSyncApp.Core.Domain;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IFileSyncEventHandler
    {
        Task HandleAsync(FileChangeEvent fileSyncEvent);
        Task HandleAsync(FileChangeEvent fileSyncEvent, int retryLimit);
    }
}
