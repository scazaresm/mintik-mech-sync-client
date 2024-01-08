using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync.Handlers;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync
{
    public class MechSyncServiceClient : IMechSyncServiceClient, IDisposable
    {
        #region Singleton
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
            AuthenticationService = AuthenticationServiceClient.Instance;

            // use this client for regular data transactions (not file upload/download)
            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri("http://localhost/api/mech-sync/");
            _restClient.Timeout = TimeSpan.FromSeconds(20);
            _restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationToken);

            // use a separate client for file upload/download operations, so we can use a higher timeout
            _fileClient = new HttpClient();
            _fileClient.BaseAddress = new Uri("http://localhost/api/mech-sync/");
            _fileClient.Timeout = TimeSpan.FromSeconds(120);
            _fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationToken);

            // subscribe to authentication token refresh event, so that we can refresh the tokens on local clients
            AuthenticationService.OnAuthenticationTokenRefresh += AuthenticationService_OnAuthenticationTokenRefresh;
        }

        private void AuthenticationService_OnAuthenticationTokenRefresh(object sender, RefreshAuthenticationTokenEventArgs e)
        {
            _restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.NewToken);
            _fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.NewToken);
        }
        #endregion

        public IAuthenticationServiceClient AuthenticationService { get; private set; }

        private readonly HttpClient _restClient;
        private readonly HttpClient _fileClient;
        private bool _disposedValue;

        public async Task DownloadFileAsync(DownloadFileRequest request, Action<int> progressCallback)
        {
            var handler = new DownloadFileWithProgressHandler(_fileClient, request, progressCallback);
            await handler.HandleAsync();
        }
        public async Task DownloadFileAsync(DownloadFileRequest request)
        {
            var handler = new DownloadFileHandler(_fileClient, request);
            await handler.HandleAsync();
        }
        public async Task UploadFileAsync(UploadFileRequest request)
        {
            var handler = new UploadFileHandler(_fileClient, request);
            await handler.HandleAsync();
        }
        public async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
        {
            var handler = new DeleteFileHandler(_fileClient, request);
            var response = await handler.HandleAsync();
            return response;
        }
        public async Task<FileMetadata> GetFileMetadataAsync(string fileMetadataId)
        {
            var handler = new GetFileMetadataByIdHandler(_restClient, fileMetadataId);
            return await handler.HandleAsync();
        }
        public async Task<List<FileMetadata>> GetFileMetadataAsync(string versionId, string relativeFilePath)
        {
            var handler = new GetVersionFileMetadataHandler(_restClient, versionId, relativeFilePath);
            return await handler.HandleAsync();
        }
        public async Task<List<FileMetadata>> GetDeltaFileMetadataAsync(GetDeltaFileMetadataRequest request)
        {
            var handler = new GetDeltaFileMetadataHandler(_restClient, request);
            var response = await handler.HandleAsync();
            return response;
        }
        public async Task<List<Version>> GetMyWorkAsync()
        {
            return await new GetMyWorkHandler(_restClient).HandleAsync();
        }
        public async Task<List<Review>> GetMyReviewsAsync()
        {
            return await new GetMyReviewsHandler(_restClient).HandleAsync();
        }
        public async Task<Review> SyncReviewTargetsAsync(string reviewId)
        {
            return await new SyncReviewTargetsHandler(_restClient, reviewId).HandleAsync();
        }
        public async Task<Project> CreateProjectAsync(CreateProjectRequest request)
        {
            return await new CreateProjectHandler(_restClient, request).HandleAsync();
        }
        public async Task<Project> GetProjectAsync(string projectId)
        {
            return await new GetProjectHandler(_restClient, projectId).HandleAsync();
        }
        public async Task<Version> GetVersionAsync(string versionId)
        {
            return await new GetVersionHandler(_restClient, versionId).HandleAsync();
        }
        public async Task<Version> TransferVersionOwnershipAsync(TransferVersionOwnershipRequest request)
        {
            return await new TransferVersionOwnershipHandler(_restClient, request).HandleAsync();
        }
        public async Task<Version> AcknowledgeVersionOwnershipAsync(AcknowledgeVersionOwnershipRequest request)
        {
            return await new AcknowledgeVersionOwnershipHandler(_restClient, request).HandleAsync();
        }
        public async Task<Version> PublishVersionAsync(PublishVersionRequest request)
        {
            return await new PublishVersionHandler(_restClient, request).HandleAsync();
        }
        public async Task<ReviewTarget> UpdateReviewTargetAsync(UpdateReviewTargetRequest request)
        {
            return await new UpdateReviewTargetHandler(_restClient, request).HandleAsync();
        }

      
        #region Disposing pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _restClient.Dispose();
                    _fileClient.Dispose();
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
