using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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
        Task<FileMetadata> UploadFileAsync(UploadFileRequest request);
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);

        Task<GetFileMetadataResponse> GetFileMetadataAsync(GetFileMetadataRequest request);

        Task<GetMyOngoingVersionsResponse> GetMyOngoingVersionsAsync();
        Task<Project> GetProjectAsync(string projectId);
    }
}
