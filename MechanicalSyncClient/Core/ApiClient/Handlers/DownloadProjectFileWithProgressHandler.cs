using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.ApiClient.Handlers
{
    class DownloadProjectFileWithProgressHandler
    {
        private readonly HttpClient _client;
        private readonly string _endpoint;
        private readonly string _localFilename;
        private readonly Action<int> _progressCallback;

        public DownloadProjectFileWithProgressHandler(
            HttpClient client,
            string endpoint,
            string localFilename,
            Action<int> progressCallback
            )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            _localFilename = localFilename ?? throw new ArgumentNullException(nameof(localFilename));
            _progressCallback = progressCallback ?? throw new ArgumentNullException(nameof(progressCallback));
        }

        public async Task HandleAsync()
        {
            using (HttpResponseMessage response = await _client.GetAsync(_endpoint, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }

                using (Stream remoteStream = await response.Content.ReadAsStreamAsync())
                using (ProgressStream progressStream = new ProgressStream(remoteStream, response.Content.Headers.ContentLength ?? 0))
                using (Stream localStream = File.Open(_localFilename, FileMode.Create))
                {
                    progressStream.ProgressChanged += (bytesRead, totalBytes) =>
                    {
                        int progress = (int)((double)bytesRead / totalBytes * 100);
                        _progressCallback(progress);
                    };
                    await progressStream.CopyToAsync(localStream);
                }
            }
        }
    }
}
