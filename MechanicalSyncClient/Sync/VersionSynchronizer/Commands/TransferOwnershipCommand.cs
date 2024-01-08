using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.UI.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class TransferOwnershipCommand : IVersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; }

        public TransferOwnershipCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            try
            {
                var userSelector = new UserSelectorForm(
                    "Transfer version ownership", 
                    "Select the user who will be the new owner for this version:"
                );
                var response = userSelector.ShowDialog();

                if (response != DialogResult.OK) return;

                var selectedUserFullName = userSelector.SelectedUserDetails.FullName;

                var confirmation = MessageBox.Show(
                    $"Transfer version ownership to {selectedUserFullName}?{Environment.NewLine}{Environment.NewLine}" +
                    "All the changes in your local copy will be automatically pushed to server (if any) and the folder will be sent to recylce bin.",
                    "Confirm ownership transfer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmation != DialogResult.Yes) return;

                var syncRemoteCommand = new SyncRemoteCommand(Synchronizer)
                {
                    ConfirmBeforeSync = false,
                    NotifyWhenComplete = false,
                };
                await syncRemoteCommand.RunAsync();

                await Synchronizer.SyncServiceClient.TransferVersionOwnershipAsync(
                    new TransferVersionOwnershipRequest
                    {
                        VersionId = Synchronizer.Version.RemoteVersion.Id,
                        UserId = userSelector.SelectedUserDetails.Id,
                        SyncChecksum = syncRemoteCommand.Summary.CalculateSyncChecksum(),
                    }
                );

                MessageBox.Show(
                    $"Successfully transferred version ownership to {selectedUserFullName}.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await Synchronizer.CloseVersionAsync();

                if (Directory.Exists(Synchronizer.Version.LocalDirectory))
                {
                    await Task.Run(() => FileSystem.DeleteDirectory(
                        Synchronizer.Version.LocalDirectory,
                        UIOption.OnlyErrorDialogs,
                        RecycleOption.SendToRecycleBin
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
