using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{

    public class MarkFileAsFixedCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;
        private readonly string ONGOING_FOLDER = "Ongoing";
        private Review review;
        private ReviewTarget reviewTarget;
        private FileMetadata reviewTargetMetadata;

        public IVersionSynchronizer Synchronizer { get; }

        public MarkFileAsFixedCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            review = Synchronizer?.CurrentFileReview ?? throw new NullReferenceException(nameof(Synchronizer.CurrentFileReview));
            reviewTarget = Synchronizer?.CurrentFileReviewTarget ?? throw new NullReferenceException(nameof(Synchronizer.CurrentFileReviewTarget));
            reviewTargetMetadata = Synchronizer.CurrentFileReviewTargetMetadata ?? throw new NullReferenceException(nameof(Synchronizer.CurrentFileReviewTargetMetadata));
        }

        public async Task RunAsync()
        {
            var ui = Synchronizer.UI;
            var grid = ui.FileChangeRequestGrid;

            try
            {
                ui.MarkFileAsFixedButton.Enabled = false;

                // check for pending change requests
                var pendingChanges = grid.Rows.Cast<DataGridViewRow>().Where(
                   (row) => (row.Tag as ChangeRequest).Status == "Pending"
                ).ToList();

                if (pendingChanges.Any())
                {
                    MessageBox.Show(
                        "There are pending change requests on this review, please mark all them either as 'Done' or 'Discarded'.",
                        "Pending change requests",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }

                var relativeFilePath = reviewTargetMetadata.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/');
                var localFilePath = Path.Combine(Synchronizer.Version.LocalDirectory, relativeFilePath);

                // sync remote to push all changes to server
                var syncRemoteCommand = new SyncRemoteCommand(Synchronizer, logger)
                {
                    NotifyWhenComplete = false,
                    EnableToolStripWhenComplete = false,
                };

                // when marking a drawing as fixed we only need to sync any file having the same file name as drawing
                // (i.e. both drawing file and its corresponding part file)
                // for assemblies we must do a complete sync, since many files might have been changed and there is no way to determine
                if (review.TargetType == ReviewTargetType.DrawingFile.ToString())
                {
                    syncRemoteCommand.TargetFileName = Path.GetFileNameWithoutExtension(localFilePath);
                }
                await syncRemoteCommand.RunAsync();

                if (!syncRemoteCommand.Complete)
                {
                    MessageBox.Show(
                        "Your local copy has changes and needs to be synced with the remote server before marking files as fixed, please try again.",
                        "Local copy has changes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }

                var response = MessageBox.Show(
                        "Are you sure to mark this file as fixed?",
                        "Mark as fixed", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                if (response != DialogResult.Yes) return;
                
                // mark review target as fixed
                await Synchronizer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = review.Id,
                    ReviewTargetId = reviewTarget.Id,
                    Status = "Fixed"
                });

                Synchronizer.UI.FileReviewsSplit.Panel2Collapsed = true;
                await Synchronizer.UI.ReviewsExplorer.Refresh();
            }
            catch(Exception ex)
            {
                var message = $"Could not mark file as fixed: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.MarkFileAsFixedButton.Enabled = true;
            }
        }
    }
}
