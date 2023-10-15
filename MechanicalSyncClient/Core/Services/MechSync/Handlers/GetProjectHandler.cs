using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public class GetProjectHandler
    {
        private readonly HttpClient client;
        private readonly string projectId;

        public GetProjectHandler(HttpClient client, string projectId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }
        
        public async Task<Project> HandleAsync()
        {
            string uri = $"projects/{projectId}";

            HttpResponseMessage response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<Project>(responseContent);
        }
    }
}
