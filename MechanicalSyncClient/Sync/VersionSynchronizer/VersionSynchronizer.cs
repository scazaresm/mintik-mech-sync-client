﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizer : IVersionSynchronizer
    {
        public IMechSyncServiceClient ServiceClient { get; set; }
        public IVersionChangeMonitor ChangeMonitor { get; private set; }

        public ConcurrentDictionary<string, FileMetadata> LocalFileIndex { get; set; }
        public ConcurrentDictionary<string, FileMetadata> RemoteFileIndex { get; private set; }

        public VersionSynchronizerUI UI { get; private set; }

        private VersionSynchronizerState state;
        private bool disposedValue;

        public OngoingVersion Version { get; }

        public string FileExtensionFilter { get; private set; } = "*.sldasm | *.sldprt | *.slddrw";

        public VersionSynchronizer(OngoingVersion version, VersionSynchronizerUI ui)
        {
            if (version is null)
            {
                throw new ArgumentNullException(nameof(version));
            }
            Version = version;

            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            ChangeMonitor = new VersionChangeMonitor(version, FileExtensionFilter);
            ServiceClient = MechSyncServiceClient.Instance;
            LocalFileIndex = new ConcurrentDictionary<string, FileMetadata>();
            RemoteFileIndex = new ConcurrentDictionary<string, FileMetadata>();

            SetState(new IdleState());
            _ = RunStepAsync();
        }


        #region State management

        public VersionSynchronizerState GetState()
        {
            return state;
        }

        public void SetState(VersionSynchronizerState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetSynchronizer(this);
            this.state.UpdateUI();
        }

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
        }

        #endregion

        #region Commands

        public async Task OpenVersionAsync()
        {
            await new OpenVersionCommand(this).RunAsync();
        }

        public async Task WorkOnlineAsync()
        {
           await new WorkOnlineCommand(this).RunAsync();
        }

        public async Task WorkOfflineAsync()
        {
            await new WorkOfflineCommand(this).RunAsync();
        }

        public async Task SyncRemoteAsync()
        {
            await new SyncRemoteCommand(this).RunAsync();
        }

        public async Task CloseVersionAsync()
        {
            await new CloseVersionCommand(this).RunAsync();
        }

        public async Task PublishVersionAsync()
        {
            await new PublishVersionCommand(this).RunAsync();
        }

        public async Task TransferOwnershipAsync()
        {
            await new TransferOwnershipCommand(this).RunAsync();
        }

        #endregion

        #region UI management

        public void InitializeUI()
        {
            UI.ProjectFolderNameLabel.Text = $"{Version.RemoteProject.FolderName} V{Version.RemoteVersion.Major} (Ongoing changes)";
            UI.InitializeFileViewer(Version, ChangeMonitor);

            UI.FileViewer.AttachListView(UI.FileViewerListView);
            UI.FileViewerListView.SetDoubleBuffered();

            UI.SyncProgressBar.Visible = false;
            UI.SyncRemoteButton.Visible = true;
            UI.SyncRemoteButton.Click += SyncRemoteButton_Click;

            UI.WorkOnlineButton.Click += WorkOnlineButton_Click;
            UI.WorkOnlineButton.Visible = true;

            UI.WorkOfflineButton.Click += WorkOfflineButton_Click;
            UI.WorkOfflineButton.Visible = false;

            UI.RefreshLocalFilesButton.Click += RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click += CloseVersionButton_Click;
            UI.PublishVersionButton.Click += PublishVersionButton_Click;
            UI.TransferOwnershipButton.Click += TransferOwnershipButton_Click;
        }

        public void UpdateUI()
        {
            if (state != null)
                state.UpdateUI();
        }

        private async void WorkOnlineButton_Click(object sender, EventArgs e)
        {
            await WorkOnlineAsync();
        }

        private async void SyncRemoteButton_Click(object sender, EventArgs e)
        {
            await SyncRemoteAsync();
        }

        private async void WorkOfflineButton_Click(object sender, EventArgs e)
        {
            await WorkOfflineAsync();
        }

        private void RefreshLocalFilesButton_Click(object sender, EventArgs e)
        {
            UI.FileViewer.PopulateFiles();
        }

        private async void CloseVersionButton_Click(object sender, EventArgs e)
        {
            await CloseVersionAsync();
        }

        private async void PublishVersionButton_Click(object sender, EventArgs e)
        {
            await PublishVersionAsync();
        }

        private async void TransferOwnershipButton_Click(object sender, EventArgs e)
        {
            await TransferOwnershipAsync();
        }

        #endregion

        #region Disposing and shutdown

        private void RemoveEventListeners()
        {
            UI.WorkOnlineButton.Click -= WorkOnlineButton_Click;
            UI.WorkOfflineButton.Click -= WorkOfflineButton_Click;
            UI.SyncRemoteButton.Click -= SyncRemoteButton_Click;
            UI.RefreshLocalFilesButton.Click -= RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click -= CloseVersionButton_Click;
            UI.PublishVersionButton.Click -= PublishVersionButton_Click;
            UI.TransferOwnershipButton.Click -= TransferOwnershipButton_Click;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    RemoveEventListeners();
                    ChangeMonitor.Dispose();
                    UI.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
