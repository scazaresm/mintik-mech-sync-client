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
    public class AcknowledgeVersionOwnershipHandler
    {
        private readonly HttpClient client;
        private readonly AcknowledgeVersionOwnershipRequest request;

        public AcknowledgeVersionOwnershipHandler(HttpClient client, AcknowledgeVersionOwnershipRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<Models.Version> HandleAsync()
        {
            string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

            HttpResponseMessage response = await client.PostAsync(
                "versions/ongoing/ownership/acknowledge",
                new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            );
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<Models.Version>(responseContent);
        }
    }
}
