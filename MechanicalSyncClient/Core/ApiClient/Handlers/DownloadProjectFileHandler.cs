using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.ApiClient.Handlers
{
    class DownloadProjectFileHandler
    {
        private readonly HttpClient _client;
        private readonly string _endpoint;
        private readonly string _localFilename;

        public DownloadProjectFileHandler(
            HttpClient client,
            string endpoint,
            string localFilename
            )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            _localFilename = localFilename ?? throw new ArgumentNullException(nameof(localFilename));
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
                using (Stream localStream = File.Open(_localFilename, FileMode.Create))
                {
                    await remoteStream.CopyToAsync(localStream);
                }
            }
        }
    }
}
