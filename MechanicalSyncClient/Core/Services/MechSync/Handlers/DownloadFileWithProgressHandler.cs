using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    class DownloadFileWithProgressHandler
    {
        private readonly HttpClient _client;
        private readonly DownloadFileRequest _request;
        private readonly Action<int> _progressCallback;

        public DownloadFileWithProgressHandler(
            HttpClient client,
            DownloadFileRequest request,
            Action<int> progressCallback)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _progressCallback = progressCallback ?? throw new ArgumentNullException(nameof(progressCallback));
        }

        public async Task HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "relativeEquipmentPath", _request.RelativeEquipmentPath },
                { "relativeFilePath", _request.RelativeFilePath },
                { "versionFolder", _request.VersionFolder }
            };

            var uri = new QueryUriGenerator("files", queryParameters).Generate();

            using (var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }

                using (var remoteStream = await response.Content.ReadAsStreamAsync())
                using (var progressStream = new ProgressStream(remoteStream, response.Content.Headers.ContentLength ?? 0))
                using (var localStream = new FileStream(_request.LocalFilename, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true))
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
