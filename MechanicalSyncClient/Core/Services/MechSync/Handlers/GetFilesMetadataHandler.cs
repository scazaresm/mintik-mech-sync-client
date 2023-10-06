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
    public class GetFilesMetadataHandler
    {
        private readonly HttpClient client;
        private readonly GetFilesMetadataRequest request;

        public GetFilesMetadataHandler(HttpClient client, GetFilesMetadataRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<GetFilesMetadataResponse> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "versionId", request.VersionId },
            };

            var uri = new QueryUriGenerator("files/metadata", queryParameters).Generate();
            HttpResponseMessage response = await client.GetAsync(uri);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<GetFilesMetadataResponse>(responseContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {responseJson.Error}"
                );
            }
            return responseJson;
        }
    }
}
