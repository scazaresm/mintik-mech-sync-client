using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetDeltaFileMetadataHandler
    {
        private readonly HttpClient client;
        private readonly GetDeltaFileMetadataRequest request;

        public GetDeltaFileMetadataHandler(HttpClient client, GetDeltaFileMetadataRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<List<FileMetadata>> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "sourceVersionId", request.SourceVersionId }
            };
            if (request.TargetVersionId != null)
                queryParameters.Add("targetVersionId", request.TargetVersionId);

            var uri = new QueryUriGenerator("files/metadata/delta", queryParameters).Generate();
            HttpResponseMessage response = await client.GetAsync(uri);
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
