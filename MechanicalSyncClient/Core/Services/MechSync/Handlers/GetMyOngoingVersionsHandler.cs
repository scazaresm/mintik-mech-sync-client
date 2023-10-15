using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetMyOngoingVersionsHandler
    {
        private readonly HttpClient client;

        public GetMyOngoingVersionsHandler(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<GetMyOngoingVersionsResponse> HandleAsync()
        {
            const string uri = "versions/ongoing/mine";

            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<GetMyOngoingVersionsResponse>(responseContent);
        }
    }
}
