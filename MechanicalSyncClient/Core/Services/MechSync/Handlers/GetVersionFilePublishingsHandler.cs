using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public class GetVersionFilePublishingsHandler
    {
        private readonly HttpClient client;
        private readonly string versionId;

        public GetVersionFilePublishingsHandler(HttpClient client, string versionId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.versionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
        }

        public async Task<List<FilePublishing>> HandleAsync()
        {
            return await Policy
           .Handle<TaskCanceledException>()
           .Or<HttpRequestException>()
           .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
           .ExecuteAsync(async () =>
           {
               var queryParameters = new Dictionary<string, string>
               {
                { "versionId", versionId }
               };

               var uri = new QueryUriGenerator("files/publishing", queryParameters).Generate();
               HttpResponseMessage response = await client.GetAsync(uri);
               var responseContent = await response.Content.ReadAsStringAsync();

               if (!response.IsSuccessStatusCode)
               {
                   var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                   throw new HttpRequestException(
                       $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                   );
               }
               return JsonConvert.DeserializeObject<List<FilePublishing>>(responseContent);
           });
        }
    }
}
