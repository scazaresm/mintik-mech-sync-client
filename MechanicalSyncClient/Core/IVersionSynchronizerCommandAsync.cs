﻿using MechanicalSyncApp.Sync.VersionSynchronizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IVersionSynchronizerCommandAsync
    {
        IVersionSynchronizer Synchronizer { get; }

        Task RunAsync();
    }
}
