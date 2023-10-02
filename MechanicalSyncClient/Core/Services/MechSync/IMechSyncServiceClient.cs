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

        Task DownloadFileAsync(DownloadFileRequest request, Action<int> progressCallback);
        Task DownloadFileAsync(DownloadFileRequest request);
        Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request);
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);
    }
}
