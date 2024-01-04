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
    public class GetFileMetadataByIdHandler
    {
        private readonly HttpClient client;
        private readonly string fileMetadataId;

        public GetFileMetadataByIdHandler(HttpClient client, string fileMetadataId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.fileMetadataId = fileMetadataId ?? throw new ArgumentNullException(nameof(fileMetadataId));
        }

        public async Task<FileMetadata> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "fileMetadataId", fileMetadataId },
            };

            var uri = new QueryUriGenerator("files/metadata", queryParameters).Generate();
            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<FileMetadata>(responseContent);
        }
    }
}
