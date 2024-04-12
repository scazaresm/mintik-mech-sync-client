using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{

    public interface IFileReviewerCommandAsync
    {
        IFileReviewer Reviewer { get; }

        Task RunAsync();
    }
}
