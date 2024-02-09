using MechanicalSyncApp.Core.Services.MechSync.Exceptions;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
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
    public class CreateProjectHandler
    {
        private readonly HttpClient client;
        private readonly CreateProjectRequest request;

        public CreateProjectHandler(HttpClient client, CreateProjectRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<Project> HandleAsync()
        {
            return await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

                    HttpResponseMessage response = await client.PostAsync(
                        "projects",
                        new StringContent(jsonRequest, Encoding.UTF8, "application/json")
                    );
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                        if (errorJson.Error.Contains("A project with the same folderName already exists in db") ||
                            errorJson.Error.Contains("The project folder already exists on server disk"))
                            throw new ProjectFolderAlreadyExistsException();
                        else if (errorJson.Error.Contains("folderName must contain a valid folder name"))
                            throw new InvalidProjectFolderException();

                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                        );
                    }
                    return JsonConvert.DeserializeObject<Project>(responseContent);
                });
        }

    }
}
