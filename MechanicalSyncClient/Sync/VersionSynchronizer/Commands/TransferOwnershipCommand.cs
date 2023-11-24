using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class TransferOwnershipCommand : VersionSynchronizerCommandAsync
    {
        public VersionSynchronizer Synchronizer { get; }

        public TransferOwnershipCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            try
            {
                var userSelector = new UserSelectorForm();
                var response = userSelector.ShowDialog();

                if (response != DialogResult.OK) return;

                var selectedUsername = userSelector.SelectedUserDetails.Username;

                var confirmation = MessageBox.Show(
                    $"Transfer version ownership to {selectedUsername}?{Environment.NewLine}{Environment.NewLine}" +
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

                await MechSyncServiceClient.Instance.TransferVersionOwnershipAsync(
                    new TransferVersionOwnershipRequest
                    {
                        VersionId = Synchronizer.Version.RemoteVersion.Id,
                        Username = userSelector.SelectedUserDetails.Username
                    }
                );

                MessageBox.Show(
                    $"Successfully transferred version ownership to {selectedUsername}.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await Synchronizer.CloseVersionAsync();

                if(Directory.Exists(Synchronizer.Version.LocalDirectory))
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
