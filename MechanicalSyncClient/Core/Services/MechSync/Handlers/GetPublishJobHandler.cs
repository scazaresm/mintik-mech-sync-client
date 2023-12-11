using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetPublishJobHandler
    {
        private readonly HttpClient client;
        private readonly string publishJobId;

        public GetPublishJobHandler(HttpClient client, string publishJobId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.publishJobId = publishJobId;
        }

        public async Task<PublishJob> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "publishJobId", publishJobId },
            };

            var uri = new QueryUriGenerator("versions/publish", queryParameters).Generate();

            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<PublishJob>(responseContent);
        }
    }
}
