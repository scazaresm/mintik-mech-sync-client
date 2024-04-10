using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenDrawingForViewingCommand : IVersionSynchronizerCommandAsync
    {
        private readonly Review review;
        private readonly ReviewTarget reviewTarget;
        private readonly FileMetadata drawingMetadata;
        private readonly ILogger logger;
        private static Dictionary<string, UserDetails> userDetailsIndex = new Dictionary<string, UserDetails>();

        public static string TempDownloadedMarkupFile { get; set; } 

        public static string TempDownloadedDrawingFile { get; set; }

        public string[] StatusesHavingMarkupFile { get; } = new string[]
        {
            "Reviewing",
            "Rejected",
            "Fixed"
        };

        public IVersionSynchronizer Synchronizer { get; private set; }

        public OpenDrawingForViewingCommand(IVersionSynchronizer synchronizer,
                                            OpenReviewTargetForViewingEventArgs e,
                                            ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            review = e.Review;
            reviewTarget = e.ReviewTarget;
            drawingMetadata = e.Metadata;
        }

        public async Task RunAsync()
        {
            logger.Debug("OpenDrawingForViewingCommand started...");
            var ui = Synchronizer.UI;
            try
            {
                ui.DrawingReviewsSplit.Panel2Collapsed = false;

                Synchronizer.CurrentDrawingReview = review;
                Synchronizer.CurrentDrawingReviewTarget = reviewTarget;

                logger.Debug("Preparing UI elements...");
                ui.DrawingReviewerStatusText.Text = "Opening drawing...";
                ui.SetDeliverableStatusText(ui.DrawingReviewerDrawingStatus, reviewTarget.Status);
                ui.DrawingReviewsTreeView.Enabled = false;
                ui.DrawingReviewerProgress.Visible = true;

                logger.Debug("Getting reviewer details...");
                var reviewerDetails = await GetReviewerDetailsAsync(review.ReviewerId);
                ui.DrawingReviewerTitle.Text = $"Drawing {Path.GetFileName(drawingMetadata.RelativeFilePath)} reviewed by {reviewerDetails.FullName}";
                ui.DrawingReviewerTitle.Visible = true;
                logger.Debug(ui.DrawingReviewerTitle.Text);

                ui.MarkDrawingAsFixedButton.Visible = reviewTarget.Status.ToLower() == "rejected";

                if (ui.DrawingReviewer != null)
                {
                    logger.Debug("Disposing existing DrawingReviewer instance before creating a new one...");
                    ui.DrawingReviewerPanel.Controls.Remove(ui.DrawingReviewer.HostControl);
                    ui.DrawingReviewer.Dispose();
                }
                logger.Debug("Downloading drawing file...");
                await DownloadDrawingFileAsync();

                if (StatusesHavingMarkupFile.Contains(reviewTarget.Status))
                {
                    logger.Debug("Downloading drawing markup file...");
                    await DownloadMarkupFileAsync();

                    logger.Debug("Instantiating DrawingReviewerControl with markup file...");
                    ui.DrawingReviewer = new DrawingReviewerControl(TempDownloadedDrawingFile, TempDownloadedMarkupFile)
                    {
                        DeleteFilesOnDispose = true
                    };
                }
                else
                {
                    logger.Debug("Instantiating DrawingReviewerControl without markup file...");
                    ui.DrawingReviewer = new DrawingReviewerControl(TempDownloadedDrawingFile)
                    {
                        DeleteFilesOnDispose = true
                    };
                }
                ui.DrawingReviewerPanel.Controls.Add(ui.DrawingReviewer.HostControl);
            }
            catch(Exception ex)
            {
                var message = $"Could not open drawing for viewing, {ex}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.DrawingReviewsTreeView.Enabled = true;
                ui.DrawingReviewerProgress.Visible = false;
                ui.DrawingReviewerStatusText.Text = "Ready";
                logger.Debug("OpenDrawingForViewingCommand complete.");
            }
        }

        private async Task DownloadDrawingFileAsync()
        {
            // download the drawing file from server and save it to a new temporary file
            TempDownloadedDrawingFile = PathUtils.GetTempFileNameWithExtension(".slddrw");

            await Synchronizer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
            {
                LocalFilename = TempDownloadedDrawingFile,
                RelativeEquipmentPath = Synchronizer.Version.RemoteProject.RelativeEquipmentPath,
                RelativeFilePath = drawingMetadata.RelativeFilePath,
                VersionFolder = "Ongoing"
            }, ReportDownloadProgress);
        }

        private async Task DownloadMarkupFileAsync()
        {
            // download the markup file from server and save it to a new temporary file
            TempDownloadedMarkupFile = PathUtils.GetTempFileNameWithExtension(".markup");

            await Synchronizer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
            {
                VersionFolder = "Markup",
                RelativeEquipmentPath = Synchronizer.Version.RemoteProject.RelativeEquipmentPath,
                LocalFilename = TempDownloadedMarkupFile,
                RelativeFilePath = $"{reviewTarget.TargetId}.markup"
            }, ReportDownloadProgress);
        }

        private async Task<UserDetails> GetReviewerDetailsAsync(string userId)
        {
            if (!userDetailsIndex.ContainsKey(userId))
                userDetailsIndex[userId] = await Synchronizer.AuthServiceClient.GetUserDetailsAsync(userId);

            return userDetailsIndex[userId];
        }

        public void ReportDownloadProgress(int progress)
        {
            var drawingReviewerProgress = Synchronizer.UI.DrawingReviewerProgress;

            if (drawingReviewerProgress.GetCurrentParent().InvokeRequired)
            {
                drawingReviewerProgress.GetCurrentParent().Invoke(
                    new Action<int>(ReportDownloadProgress),
                    progress
                );
            }
            else
            {
                drawingReviewerProgress.Value = progress;
            }
        }

    }
}
