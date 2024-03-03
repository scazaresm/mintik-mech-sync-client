using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync.Handlers;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync
{
    public class MechSyncServiceClient : IMechSyncServiceClient, IDisposable
    {
        #region Singleton

        private readonly string SERVER_URL = ConfigurationManager.AppSettings["SERVER_URL"];
        private readonly string DEFAULT_TIMEOUT_SECONDS = ConfigurationManager.AppSettings["DEFAULT_TIMEOUT_SECONDS"];

        private static MechSyncServiceClient _instance = null;

        public static MechSyncServiceClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MechSyncServiceClient();
                return _instance;
            }
        }

        private MechSyncServiceClient()
        {
            try
            {
                AuthenticationService = AuthenticationServiceClient.Instance;

                var defaultTimeoutSeconds = double.Parse(DEFAULT_TIMEOUT_SECONDS);

                // use this client for regular data transactions (not file upload/download)
                restClient = new HttpClient(new VerboseHandler(new HttpClientHandler()));
                restClient.BaseAddress = new Uri($"{SERVER_URL}/api/mech-sync/");
                restClient.Timeout = TimeSpan.FromSeconds(defaultTimeoutSeconds);
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationToken);

                // use a separate client for file upload/download operations, so we can use a higher timeout
                fileClient = new HttpClient(new VerboseHandler(new HttpClientHandler()));
                fileClient.BaseAddress = new Uri($"{SERVER_URL}/api/mech-sync/");
                fileClient.Timeout = TimeSpan.FromSeconds(120);
                fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationToken);

                // subscribe to authentication token refresh event, so that we can refresh the tokens on local clients
                AuthenticationService.OnAuthenticationTokenRefresh += AuthenticationService_OnAuthenticationTokenRefresh;
            }
            catch (FormatException ex)
            {
                throw new Exception("Check data types for SERVER_URL (string) and DEFAULT_TIMEOUT_SECONDS (double as string) in config file.", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AuthenticationService_OnAuthenticationTokenRefresh(object sender, RefreshAuthenticationTokenEventArgs e)
        {
            restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.NewToken);
            fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.NewToken);
        }
        #endregion

        public IAuthenticationServiceClient AuthenticationService { get; private set; }

        private readonly HttpClient restClient;
        private readonly HttpClient fileClient;
        private bool _disposedValue;

        public async Task DownloadFileAsync(DownloadFileRequest request, Action<int> progressCallback)
        {
            var handler = new DownloadFileWithProgressHandler(fileClient, request, progressCallback);
            await handler.HandleAsync();
        }
        public async Task DownloadFileAsync(DownloadFileRequest request)
        {
            var handler = new DownloadFileHandler(fileClient, request);
            await handler.HandleAsync();
        }
        public async Task UploadFileAsync(UploadFileRequest request)
        {
            var handler = new UploadFileHandler(fileClient, request);
            await handler.HandleAsync();
        }
        public async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
        {
            var handler = new DeleteFileHandler(fileClient, request);
            var response = await handler.HandleAsync();
            return response;
        }
        public async Task<FileMetadata> GetFileMetadataAsync(string fileMetadataId)
        {
            var handler = new GetFileMetadataByIdHandler(restClient, fileMetadataId);
            return await handler.HandleAsync();
        }
        public async Task<List<FileMetadata>> GetFileMetadataAsync(string versionId, string relativeFilePath)
        {
            var handler = new GetVersionFileMetadataHandler(restClient, versionId, relativeFilePath);
            return await handler.HandleAsync();
        }
        public async Task<List<FileMetadata>> GetDeltaFileMetadataAsync(GetDeltaFileMetadataRequest request)
        {
            var handler = new GetDeltaFileMetadataHandler(restClient, request);
            var response = await handler.HandleAsync();
            return response;
        }
        public async Task<List<Version>> GetMyWorkAsync()
        {
            return await new GetMyWorkHandler(restClient).HandleAsync();
        }
        public async Task<List<Review>> GetMyReviewsAsync()
        {
            return await new GetMyReviewsHandler(restClient).HandleAsync();
        }
        public async Task<Review> SyncReviewTargetsAsync(string reviewId)
        {
            return await new SyncReviewTargetsHandler(restClient, reviewId).HandleAsync();
        }
        public async Task<Project> CreateProjectAsync(CreateProjectRequest request)
        {
            return await new CreateProjectHandler(restClient, request).HandleAsync();
        }
        public async Task<Review> CreateReviewAsync(CreateReviewRequest request)
        {
            return await new CreateReviewHandler(restClient, request).HandleAsync();
        }
        public async Task<Project> GetProjectAsync(string projectId)
        {
            return await new GetProjectHandler(restClient, projectId).HandleAsync();
        }
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await new GetAllProjectsHandler(restClient).HandleAsync(); 
        }
        public async Task<List<Project>> GetPublishedProjectsAsync()
        {
            return await new GetPublishedProjectsHandler(restClient).HandleAsync();
        }
        public async Task<Version> CreateVersionAsync(CreateVersionRequest request)
        {
            return await new CreateVersionHandler(restClient, request).HandleAsync();
        }
        public async Task<List<Version>> GetAllVersionsAsync()
        {
            return await new GetAllVersionsHandler(restClient).HandleAsync();
        }
        public async Task<List<Version>> GetVersionsWithStatusAsync(string status)
        {
            return await new GetVersionsWithStatusHandler(restClient, status).HandleAsync();
        }
        public async Task<Version> GetVersionAsync(string versionId)
        {
            return await new GetVersionHandler(restClient, versionId).HandleAsync();
        }
        public async Task<Version> TransferVersionOwnershipAsync(TransferVersionOwnershipRequest request)
        {
            return await new TransferVersionOwnershipHandler(restClient, request).HandleAsync();
        }
        public async Task<Version> AcknowledgeVersionOwnershipAsync(AcknowledgeVersionOwnershipRequest request)
        {
            return await new AcknowledgeVersionOwnershipHandler(restClient, request).HandleAsync();
        }
        public async Task<Version> PublishVersionAsync(PublishVersionRequest request)
        {
            return await new PublishVersionHandler(restClient, request).HandleAsync();
        }
        public async Task<ReviewTarget> UpdateReviewTargetAsync(UpdateReviewTargetRequest request)
        {
            return await new UpdateReviewTargetHandler(restClient, request).HandleAsync();
        }
        public async Task<List<Review>> GetVersionReviewsAsync(string versionId)
        {
            return await new GetVersionReviewsHandler(restClient, versionId).HandleAsync();
        }

        public async Task<List<AggregatedProjectDetails>> AggregateProjectDetailsAsync()
        {
            return await new AggregateProjectDetailsHandler(restClient).HandleAsync();
        }

        public async Task<Review> GetReviewAsync(string reviewId)
        {
            return await new GetReviewHandler(restClient, reviewId).HandleAsync();
        }

        public async Task DeleteExplorerFilesAsync(string relativeEquipmentPath, string explorerTransactionId)
        {
            await new DeleteExplorerFilesHandler(
                restClient, 
                relativeEquipmentPath, 
                explorerTransactionId
            ).HandleAsync();
        }

        #region Disposing pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    restClient.Dispose();
                    fileClient.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
        #endregion
    }
}
