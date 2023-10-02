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
        private readonly IChecksumValidator checksumValidator;

        public UploadFileHandler(
            HttpClient client, 
            UploadFileRequest request,
            IChecksumValidator checksumValidator
            )
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client)); ;
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.checksumValidator = checksumValidator ?? throw new ArgumentNullException(nameof(checksumValidator));
        }

        public async Task<UploadFileResponse> HandleAsync()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                File.Copy(request.LocalFilePath, tempFile, true);

                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = File.OpenRead(tempFile))
                    {
                        string fileChecksum = checksumValidator.CalculateFromFile(tempFile);
                        formData.Headers.Add("X-Checksum-SHA256", fileChecksum);

                        formData.Add(new StreamContent(fileStream), "file", Path.GetFileName(request.LocalFilePath));
                        formData.Add(new StringContent(request.RelativeFilePath), "relativeFilePath");
                        formData.Add(new StringContent(request.ProjectId), "projectId");

                        var response = await client.PostAsync("files", formData);
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseJson = JsonConvert.DeserializeObject<UploadFileResponse>(responseContent);

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException(
                                $"HTTP request failed with status code {response.StatusCode}, {responseJson.Error}"
                            );
                        }
                        return responseJson;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not upload file: {ex.Message}", ex);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
