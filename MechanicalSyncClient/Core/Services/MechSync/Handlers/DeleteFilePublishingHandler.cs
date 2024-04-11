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
    public class DeleteFilePublishingHandler
    {
        private readonly HttpClient client;
        private readonly string publishingId;

        public DeleteFilePublishingHandler(HttpClient client, string publishingId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.publishingId = publishingId ?? throw new ArgumentNullException(nameof(publishingId));
        }

        public async Task HandleAsync()
        {
            await Policy
               .Handle<TaskCanceledException>()
               .Or<HttpRequestException>()
               .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
               .ExecuteAsync(async () =>
               {
                   var queryParameters = new Dictionary<string, string>
                   {
                    { "publishingId", publishingId },
                   };

                   var uri = new QueryUriGenerator("files/publishing", queryParameters).Generate();
                   HttpResponseMessage response = await client.DeleteAsync(uri);

                   var responseContent = await response.Content.ReadAsStringAsync();

                   if (!response.IsSuccessStatusCode)
                   {
                       var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                       throw new HttpRequestException(
                           $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                       );
                   }
               });
        }
    }
}
