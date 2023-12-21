using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;
using MechanicalSyncApp.Core.Util;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetVersionHandler
    {
        private readonly HttpClient client;
        private readonly string versionId;

        public GetVersionHandler(HttpClient client, string versionId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.versionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
        }

        public async Task<Version> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "versionId", versionId },
            };

            var uri = new QueryUriGenerator("versions", queryParameters).Generate();
            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<Version>(responseContent);
        }
    }
}
