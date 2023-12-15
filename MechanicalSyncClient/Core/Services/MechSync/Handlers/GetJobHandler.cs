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
    public class GetJobHandler
    {
        private readonly HttpClient client;
        private readonly string jobId;

        public GetJobHandler(HttpClient client, string jobId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.jobId = jobId;
        }

        public async Task<Job> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "jobId", jobId },
            };

            var uri = new QueryUriGenerator("jobs", queryParameters).Generate();

            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<Job>(responseContent);
        }
    }
}
