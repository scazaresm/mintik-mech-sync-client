using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class DeleteFileHandler
    {
        private readonly HttpClient client;
        private readonly DeleteFileRequest request;

        public DeleteFileHandler(HttpClient client, DeleteFileRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<DeleteFileResponse> HandleAsync()
        {
            return await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    var queryParameters = new Dictionary<string, string>
                    {
                        { "versionId", request.VersionId },
                        { "versionFolder", request.VersionFolder },
                        { "relativeEquipmentPath", request.RelativeEquipmentPath },
                        { "relativeFilePath", request.RelativeFilePath }
                    };

                    var uri = new QueryUriGenerator("files", queryParameters).Generate();
                    HttpResponseMessage response = await client.DeleteAsync(uri);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                        );
                    }
                    return JsonConvert.DeserializeObject<DeleteFileResponse>(responseContent);
                });
        }

    }
}
