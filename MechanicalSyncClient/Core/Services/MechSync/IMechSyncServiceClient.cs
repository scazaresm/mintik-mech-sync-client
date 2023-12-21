using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using System;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

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
        Task<Version> TransferVersionOwnershipAsync(TransferVersionOwnershipRequest request);
        Task<Version> AcknowledgeVersionOwnershipAsync(AcknowledgeVersionOwnershipRequest request);
        Task<GetMyOngoingVersionsResponse> GetMyOngoingVersionsAsync();
        Task<Project> GetProjectAsync(string projectId);
        Task<Version> GetVersionAsync(string versionId);
        Task<Version> PublishVersionAsync(PublishVersionRequest request);
    }
}
