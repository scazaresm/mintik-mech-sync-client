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
        private readonly IChecksumValidator _checksumCalculator;
        private readonly Action<int> _progressCallback;

        public DownloadFileWithProgressHandler(
            HttpClient client,
            DownloadFileRequest request,
            IChecksumValidator checksumCalculator,
            Action<int> progressCallback)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _checksumCalculator = checksumCalculator ?? throw new ArgumentNullException(nameof(checksumCalculator));
            _progressCallback = progressCallback ?? throw new ArgumentNullException(nameof(progressCallback));
        }

        public async Task HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "relativeFilePath", _request.RelativeFilePath },
                { "projectId", _request.ProjectId },
                { "versionFolder", _request.VersionFolder }
            };

            var uri = new QueryUriGenerator("files", queryParameters).Generate();

            using (HttpResponseMessage response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }

                using (Stream remoteStream = await response.Content.ReadAsStreamAsync())
                using (ProgressStream progressStream = new ProgressStream(remoteStream, response.Content.Headers.ContentLength ?? 0))
                using (Stream localStream = File.Open(_request.LocalFilename, FileMode.Create))
                {
                    progressStream.ProgressChanged += (bytesRead, totalBytes) =>
                    {
                        int progress = (int)((double)bytesRead / totalBytes * 100);
                        _progressCallback(progress);
                    };
                    await progressStream.CopyToAsync(localStream);
                }

                if (response.Headers.Contains("X-Checksum-SHA256"))
                {
                    string expectedChecksum = response.Headers.GetValues("X-Checksum-SHA256").FirstOrDefault();
                    if (!string.IsNullOrEmpty(expectedChecksum))
                    {
                        _checksumCalculator.ValidateFileChecksum(_request.LocalFilename, expectedChecksum);
                    }
                }
            }
        }
    }
}
