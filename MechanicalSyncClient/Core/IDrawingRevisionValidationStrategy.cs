﻿using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IDrawingRevisionValidationStrategy
    {
        Task ValidateAsync(FileMetadata drawing);
    }
}
