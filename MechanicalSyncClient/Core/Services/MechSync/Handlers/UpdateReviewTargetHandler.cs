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
    public class UpdateReviewTargetHandler
    {
        private readonly HttpClient client;
        private readonly UpdateReviewTargetRequest request;

        public UpdateReviewTargetHandler(HttpClient client, UpdateReviewTargetRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<ReviewTarget> HandleAsync()
        {
            string endpointUrl = $"versions/reviews/{request.ReviewId}/targets/{request.ReviewTargetId}";

            string jsonRequest = JsonUtils.SerializeWithCamelCase(new
            {
                request.Status
            });

            HttpResponseMessage response = await client.PutAsync(
                endpointUrl, 
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
            return JsonConvert.DeserializeObject<ReviewTarget>(responseContent);
        }
    }
}
