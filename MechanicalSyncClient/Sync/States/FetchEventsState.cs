﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Database;
using MechanicalSyncApp.Database.Domain;
using System;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.States
{
    public class FetchEventsState : ProjectSynchronizerState
    {
        public override void UpdateUI()
        {
            throw new NotImplementedException();
        }

        public override async Task RunTransitionLogicAsync()
        {
           

        }
    }
}
