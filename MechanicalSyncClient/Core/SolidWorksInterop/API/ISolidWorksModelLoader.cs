using Serilog;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop.API
{
    public interface ISolidWorksModelLoader
    {
        ModelDoc2 LoadModel(string filePath);
        Task<ModelDoc2> LoadModelAsync(string filePath);
        void UnloadModel(ModelDoc2 model);
    }
}
