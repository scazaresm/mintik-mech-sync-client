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
        private readonly HttpClient _client;
        private readonly UploadFileRequest _request;
        private readonly IChecksumValidator _checksumValidator;

        public UploadFileHandler(
            HttpClient client, 
            UploadFileRequest request,
            IChecksumValidator checksumValidator
            )
        {
            _client = client ?? throw new ArgumentNullException(nameof(client)); ;
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _checksumValidator = checksumValidator ?? throw new ArgumentNullException(nameof(checksumValidator));
        }

        public async Task<UploadFileResponse> HandleWithRetryAsync(int retryCount)
        {
            if(retryCount < 1 || retryCount > 10)
            {
                throw new ArgumentException($"{nameof(retryCount)} must be a number between 1 and 10");
            }
            int retryNumber = 0;
            bool success = false;

            UploadFileResponse response = null;
            Exception lastException = null;

            do
            {
                try
                {
                    response = await HandleAsync();
                    success = true;
                }
                catch(Exception ex)
                {
                    lastException = ex;
                    success = false;
                }
                finally
                {
                    retryNumber++;
                }
            }
            while (!success && retryNumber < retryCount);

            if(lastException != null)
                throw lastException;

            return response;
        }

        public async Task<UploadFileResponse> HandleAsync()
        {
            using (var formData = new MultipartFormDataContent())
            {
                using (var fileStream = File.OpenRead(_request.LocalFilePath))
                {
                    string fileChecksum = _checksumValidator.CalculateFromFile(_request.LocalFilePath);
                    formData.Headers.Add("X-Checksum-SHA256", fileChecksum);

                    formData.Add(new StreamContent(fileStream), "file", Path.GetFileName(_request.LocalFilePath));
                    formData.Add(new StringContent(_request.RelativeFilePath), "relativeFilePath");
                    formData.Add(new StringContent(_request.ProjectId), "projectId");

                    var response = await _client.PostAsync("files", formData);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<UploadFileResponse>(responseContent);
        
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}: {responseJson.Error}"
                        );
                    }
                    return responseJson;
                }
            }
        }
    }
}
