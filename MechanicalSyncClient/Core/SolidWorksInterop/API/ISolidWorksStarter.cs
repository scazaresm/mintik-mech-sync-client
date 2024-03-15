using Serilog;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface ISolidWorksStarter : IDisposable
    {
        Task StartSolidWorksAsync();
        void StartSolidWorks();
    }
}
