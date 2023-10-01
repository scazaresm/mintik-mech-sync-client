using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using System;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync
{
    public interface IMechSyncServiceClient
    {
        IAuthenticationServiceClient AuthenticationService { get; }

        Task DownloadProjectFileAsync(DownloadFileRequest request, Action<int> progressCallback);
        Task DownloadProjectFileAsync(DownloadFileRequest request);
        Task<UploadFileResponse> UploadProjectFileAsync(UploadFileRequest request);
        Task<DeleteFileResponse> DeleteProjectFileAsync(DeleteFileRequest request);
    }
}
