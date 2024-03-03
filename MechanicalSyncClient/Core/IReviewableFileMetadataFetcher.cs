using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    internal interface IReviewableFileMetadataFetcher
    {
        Task<List<FileMetadata>> FetchReviewableDrawingsAsync();
        Task<List<FileMetadata>> FetchReviewableAssembliesAsync();
    }
}
