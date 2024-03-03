using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync
{
    public interface IMechSyncServiceClient
    {
        IAuthenticationServiceClient AuthenticationService { get; }

        Task DownloadFileAsync(DownloadFileRequest request, Action<int> progressCallback);
        Task DownloadFileAsync(DownloadFileRequest request);
        Task UploadFileAsync(UploadFileRequest request);
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);
        Task<FileMetadata> GetFileMetadataAsync(string fileMetadataId);
        Task<List<FileMetadata>> GetFileMetadataAsync(string versionId, string relativeFilePath);
        Task<List<FileMetadata>> GetDeltaFileMetadataAsync(GetDeltaFileMetadataRequest request);
        Task<Review> SyncReviewTargetsAsync(string reviewId);
        Task<Version> TransferVersionOwnershipAsync(TransferVersionOwnershipRequest request);
        Task<Version> AcknowledgeVersionOwnershipAsync(AcknowledgeVersionOwnershipRequest request);
        Task<List<Version>> GetMyWorkAsync();
        Task<List<Review>> GetMyReviewsAsync();
        Task<List<Project>> GetAllProjectsAsync();
        Task<Project> CreateProjectAsync(CreateProjectRequest request);
        Task<Project> GetProjectAsync(string projectId); 
        Task<Version> CreateVersionAsync(CreateVersionRequest request);
        Task<Review> CreateReviewAsync(CreateReviewRequest request);
        Task<List<Version>> GetAllVersionsAsync();
        Task<List<Version>> GetVersionsWithStatusAsync(string status);
        Task<Version> GetVersionAsync(string versionId);
        Task<Version> PublishVersionAsync(PublishVersionRequest request);
        Task<ReviewTarget> UpdateReviewTargetAsync(UpdateReviewTargetRequest request);
        Task<List<Project>> GetPublishedProjectsAsync();
        Task<List<Review>> GetVersionReviewsAsync(string versionId);

        Task<List<AggregatedProjectDetails>> AggregateProjectDetailsAsync();

        Task<Review> GetReviewAsync(string reviewId);

        Task DeleteExplorerFilesAsync(string relativeEquipmentPath, string explorerTransactionId);
    }
}
