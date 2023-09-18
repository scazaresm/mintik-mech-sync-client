using MechanicalSyncClient.Core.ApiClient.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.ApiClient
{
    public class MechSyncClient : IMechSyncClient, IDisposable
    {
        private static MechSyncClient instance = null;

        private readonly HttpClient _client;
        private bool disposedValue;

        private MechSyncClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:3000/");
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public static MechSyncClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new MechSyncClient();
                return instance;
            }
        }

        public async Task DownloadProjectFileAsync(string endpoint, string localFilename, Action<int> progressCallback)
        {
            var handler = new DownloadProjectFileWithProgressHandler(_client, endpoint, localFilename, progressCallback);
            await handler.HandleAsync();
        }

        public async Task DownloadProjectFileAsync(string endpoint, string localFilename)
        {
            var handler = new DownloadProjectFileHandler(_client, endpoint, localFilename);
            await handler.HandleAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
