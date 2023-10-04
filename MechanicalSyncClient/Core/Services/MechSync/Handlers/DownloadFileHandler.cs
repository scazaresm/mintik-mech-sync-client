using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    class DownloadFileHandler
    {
        private readonly HttpClient _client;
        private readonly DownloadFileRequest _request;
        private readonly IChecksumValidator _checksumValidator;

        public DownloadFileHandler(HttpClient client, DownloadFileRequest request, IChecksumValidator checksumValidator)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _checksumValidator = checksumValidator ?? throw new ArgumentNullException(nameof(checksumValidator));
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
                using (Stream localStream = File.Open(_request.LocalFilename, FileMode.Create))
                {
                    await remoteStream.CopyToAsync(localStream);
                }

                string expectedChecksum = response.Headers.GetValues("X-Checksum-SHA256").FirstOrDefault();
                if (!string.IsNullOrEmpty(expectedChecksum))
                {
                    _checksumValidator.ValidateFileChecksum(_request.LocalFilename, expectedChecksum);
                }
            }
        }
    }
}
