using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.ApiClient
{
    public interface IMechSyncClient
    {
        Task DownloadProjectFileAsync(string endpoint, string localFilename, Action<int> progressCallback);
        Task DownloadProjectFileAsync(string endpoint, string localFilename);
    }
}
