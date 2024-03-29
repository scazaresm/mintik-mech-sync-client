﻿using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using Polly;
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

        public DownloadFileHandler(HttpClient client, DownloadFileRequest request)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task HandleAsync()
        {
            // Define a retry policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>() // Handle TaskCanceledException
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            // Execute the HTTP request with the retry policy
            await retryPolicy.ExecuteAsync(async () =>
            {
                var queryParameters = new Dictionary<string, string>
                {
                    { "relativeEquipmentPath", _request.RelativeEquipmentPath },
                    { "relativeFilePath", _request.RelativeFilePath },
                    { "versionFolder", _request.VersionFolder },
                };
                if(_request.ExplorerTransactionId != null)
                    queryParameters.Add("explorerTransactionId", _request.ExplorerTransactionId);

                var uri = new QueryUriGenerator("files", queryParameters).Generate();

                using (var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");

                    using (var remoteStream = await response.Content.ReadAsStreamAsync())
                    using (var localStream = new FileStream(_request.LocalFilename, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true))
                    {
                        await remoteStream.CopyToAsync(localStream);
                    }
                }
            });
        }
    }
}
