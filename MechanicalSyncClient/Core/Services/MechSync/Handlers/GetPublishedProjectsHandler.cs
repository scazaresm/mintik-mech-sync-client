﻿using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public class GetPublishedProjectsHandler
    {
        private readonly HttpClient client;

        public GetPublishedProjectsHandler(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<Project>> HandleAsync()
        {
            HttpResponseMessage response = await client.GetAsync("projects/published");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<List<Project>>(responseContent);
        }
    }
}
