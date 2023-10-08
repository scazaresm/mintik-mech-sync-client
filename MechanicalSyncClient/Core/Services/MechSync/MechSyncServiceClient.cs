using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync.Handlers;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri("http://localhost/mech-sync-service/");
            _restClient.Timeout = TimeSpan.FromSeconds(5);

            _fileClient = new HttpClient();
            _fileClient.BaseAddress = new Uri("http://localhost/mech-sync-service/");
            _fileClient.Timeout = TimeSpan.FromSeconds(120);
        }
        #endregion

        public IAuthenticationServiceClient AuthenticationService { get; private set; }

        private readonly HttpClient _restClient;
        private readonly HttpClient _fileClient;
        private bool _disposedValue;

        private async Task RefreshAuthTokenAsync()
        {
            var response = await AuthenticationService.RefreshTokenAsync();
            _restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
            _fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
        }

        public async Task DownloadFileAsync(DownloadFileRequest request, Action<int> progressCallback)
        {
            await RefreshAuthTokenAsync();
            var handler = new DownloadFileWithProgressHandler(_fileClient, request, progressCallback);
            await handler.HandleAsync();
        }

        public async Task DownloadFileAsync(DownloadFileRequest request)
        {
            await RefreshAuthTokenAsync();
            var handler = new DownloadFileHandler(_fileClient, request);
            await handler.HandleAsync();
        }

        public async Task<FileMetadata> UploadFileAsync(UploadFileRequest request)
        {
            await RefreshAuthTokenAsync();
            var handler = new UploadFileHandler(_fileClient, request);
            var response = await handler.HandleAsync();
            return response;
        }

        public async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
        {
            await RefreshAuthTokenAsync();
            var handler = new DeleteFileHandler(_fileClient, request);
            var response = await handler.HandleAsync();
            return response;
        }

        public async Task<GetFileMetadataResponse> GetFileMetadataAsync(GetFileMetadataRequest request)
        {
            await RefreshAuthTokenAsync();
            var handler = new GetFileMetadataHandler(_fileClient, request);
            var response = await handler.HandleAsync();
            return response;
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
