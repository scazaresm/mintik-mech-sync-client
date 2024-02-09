using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetVersionFileMetadataHandler
    {
        private readonly HttpClient client;
        private readonly string versionId;
        private readonly string relativeFilePath;

        public GetVersionFileMetadataHandler(HttpClient client, string versionId, string relativeFilePath)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.versionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
            this.relativeFilePath = relativeFilePath;
        }

        public async Task<List<FileMetadata>> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "versionId", versionId }
            };
            if (relativeFilePath != null)
                queryParameters.Add("relativeFilePath", relativeFilePath);

            var uri = new QueryUriGenerator("files/metadata", queryParameters).Generate();

            // Define a retry policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            // Execute the HTTP request with the retry policy
            HttpResponseMessage response = await retryPolicy.ExecuteAsync(() => client.GetAsync(uri));

            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }

            return JsonConvert.DeserializeObject<List<FileMetadata>>(responseContent);
        }
    }
}
