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
    public class CreateReviewHandler
    {
        private readonly HttpClient client;
        private readonly CreateReviewRequest request;

        public CreateReviewHandler(HttpClient client, CreateReviewRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<Review> HandleAsync()
        {
            return await Policy
               .Handle<TaskCanceledException>()
               .Or<HttpRequestException>()
               .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
               .ExecuteAsync(async () =>
               {
                   string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

                   HttpResponseMessage response = await client.PostAsync(
                       "versions/reviews",
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
                   return JsonConvert.DeserializeObject<Review>(responseContent);
               });
        } 
    }
}
