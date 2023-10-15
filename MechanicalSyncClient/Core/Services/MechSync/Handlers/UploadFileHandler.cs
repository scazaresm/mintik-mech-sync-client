using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
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

        public async Task<FileMetadata> HandleAsync()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                File.Copy(request.LocalFilePath, tempFile, true);

                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        formData.Add(new StreamContent(fileStream), "file", Path.GetFileName(request.LocalFilePath));
                        formData.Add(new StringContent(request.RelativeFilePath), "relativeFilePath");
                        formData.Add(new StringContent(request.ProjectId), "projectId");

                        var response = await client.PostAsync("files", formData);
                        var responseContent = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)
                        {
                            var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                            throw new HttpRequestException(
                                $"HTTP request failed with status code {response.StatusCode}, {errorJson.Error}"
                            );
                        }
                        return JsonConvert.DeserializeObject<FileMetadata>(responseContent); ;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not upload file: {ex.Message}", ex);
            }
            finally
            {
                if(File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
