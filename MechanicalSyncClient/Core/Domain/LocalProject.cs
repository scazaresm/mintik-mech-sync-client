using System;
using System.Collections.Generic;
using System.IO;

namespace MechanicalSyncApp.Core.Domain
{
    public class LocalProject
    {
        public string RemoteId { get; set; }
        public string LocalDirectory { get; set; }

        public LocalProject(string remoteId, string localDirectory)
        {
            RemoteId = remoteId;
            LocalDirectory = localDirectory;
        }
    }
}

