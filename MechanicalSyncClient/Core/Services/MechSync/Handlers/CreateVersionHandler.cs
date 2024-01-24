using MechanicalSyncApp.Core.Services.MechSync.Exceptions;
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
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class CreateVersionHandler
    {
        private readonly HttpClient client;
        private readonly CreateVersionRequest request;

        public CreateVersionHandler(HttpClient client, CreateVersionRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request;
        }

        public async Task<Version> HandleAsync()
        {
            string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

            HttpResponseMessage response = await client.PostAsync(
                "versions",
                new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            );
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                /*
                if (errorJson.Error.Contains("A project with the same folderName already exists in db") ||
                    errorJson.Error.Contains("The project folder already exists on server disk")
                )
                    throw new ProjectFolderAlreadyExistsException();
                else if (errorJson.Error.Contains("folderName must contain a valid folder name"))
                    throw new InvalidProjectFolderException();
                */

                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                );
            }
            return JsonConvert.DeserializeObject<Version>(responseContent);
        }
    }
}
