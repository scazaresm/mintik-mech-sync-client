﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MechanicalSyncApp.Core.Util;

namespace MechanicalSyncApp.Reviews.DrawingReviewer.Commands
{
    public class OpenDrawingReviewCommand : IDrawingReviewerCommandAsync
    {
        private readonly ReviewTarget reviewTarget;

        public IDrawingReviewer Reviewer { get; }

        public OpenDrawingReviewCommand(IDrawingReviewer drawingReviewer, ReviewTarget reviewTarget)
        {
            Reviewer = drawingReviewer ?? throw new ArgumentNullException(nameof(drawingReviewer));
            this.reviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
        }

        public async Task RunAsync()
        {
            var ui = Reviewer.UI;

            try
            {
                Reviewer.ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));

                ui.MarkupStatus.Text = "Opening drawing...";
                ui.SetReviewTargetStatusText(reviewTarget.Status);
                ui.SetReviewControlsEnabled(
                    Reviewer.StatusesHavingReviewControlsEnabled.Contains(reviewTarget.Status)
                );

                Reviewer.DrawingMetadata = await Reviewer.SyncServiceClient.GetFileMetadataAsync(reviewTarget.TargetId);
                ui.SetHeaderText(
                    $"Reviewing {Path.GetFileNameWithoutExtension(Reviewer.DrawingMetadata.RelativeFilePath)} from {Reviewer.Review}"
                );

                Reviewer.TempDownloadedDrawingFile = PathUtils.GetTempFileNameWithExtension(".slddrw");

                ui.ShowDrawingMarkupPanel();
                ui.ShowDownloadProgress();

                await Reviewer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = Reviewer.TempDownloadedDrawingFile,
                    RelativeEquipmentPath = Reviewer.Review.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = Reviewer.DrawingMetadata.RelativeFilePath,
                    VersionFolder = "Ongoing"
                }, ui.ReportDownloadProgress);

                ui.HideDownloadProgress();

                if (Reviewer.StatusesHavingMarkupFile.Contains(reviewTarget.Status))
                {
                    // download the markup file from server
                    await Reviewer.DownloadMarkupFileAsync();

                    // open drawing with markup
                    ui.LoadDrawing(
                        Reviewer.TempDownloadedDrawingFile, 
                        Reviewer.TempDownloadedMarkupFile, 
                        Reviewer.DrawingReviewerControl_OpenDocError
                    );
                }
                else
                    // open drawing without markup
                    ui.LoadDrawing(Reviewer.TempDownloadedDrawingFile, Reviewer.DrawingReviewerControl_OpenDocError);

                Reviewer.UI.MarkupStatus.Text = "Ready";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not open drawing for review, {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                await Reviewer.CloseReviewTargetAsync();
            }
        }
    }
}
