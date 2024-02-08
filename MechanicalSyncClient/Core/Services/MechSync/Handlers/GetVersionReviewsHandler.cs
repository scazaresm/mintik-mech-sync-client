using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public class GetVersionReviewsHandler
    {
        private readonly HttpClient client;
        private readonly string versionId;

        public GetVersionReviewsHandler(HttpClient client, string versionId)
        {
            this.client = client;
            this.versionId = versionId;
        }

        public async Task<List<Review>> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "versionId", versionId }
            };

            var uri = new QueryUriGenerator("versions/reviews", queryParameters).Generate();
            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<List<Review>>(responseContent);
        }
    }
}
