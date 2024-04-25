using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    class UploadFileHandler
    {
        private readonly HttpClient client;
        private readonly UploadFileRequest request;

        public UploadFileHandler(HttpClient client, UploadFileRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client)); ;
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task HandleAsync()
        {
            await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    string tempFile = Path.GetTempFileName();
                    try
                    {
                        // do not upload right away, create a temporary copy instead
                        File.Copy(request.LocalFilePath, tempFile, true);

                        using (var formData = new MultipartFormDataContent())
                        {
                            // it's important to make sure our temp file is not read-only before opening for upload
                            // windows will have conflict with temp files marked as read-only even when you open them for read
                            FileAttributes attributes = File.GetAttributes(tempFile);
                            attributes &= ~FileAttributes.ReadOnly;
                            File.SetAttributes(tempFile, attributes);

                            // open the file and upload
                            using (var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                            {
                                formData.Add(new StreamContent(fileStream), "file", Path.GetFileName(request.LocalFilePath));
                                formData.Add(new StringContent(request.VersionId), "versionId");
                                formData.Add(new StringContent(request.VersionFolder), "versionFolder");
                                formData.Add(new StringContent(request.RelativeEquipmentPath), "relativeEquipmentPath");
                                formData.Add(new StringContent(request.RelativeFilePath), "relativeFilePath");

                                var response = await client.PostAsync("files", formData);
                                var responseContent = await response.Content.ReadAsStringAsync();

                                if (!response.IsSuccessStatusCode)
                                {
                                    var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                                    throw new HttpRequestException(
                                        $"HTTP request failed with status code {response.StatusCode}, {errorJson.Error}"
                                    );
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Could not upload file: {ex.Message}", ex);
                    }
                    finally
                    {
                        if (File.Exists(tempFile))
                            File.Delete(tempFile);
                    }
                });
        }

    }
}
