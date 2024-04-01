using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Reviews.DrawingReviewer.Commands;
using MechanicalSyncApp.UI;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.DrawingReviewer
{
    public class DrawingReviewer : IDrawingReviewer
    {
        private readonly ILogger logger;

        public IDrawingReviewerUI UI { get; }

        public IAuthenticationServiceClient AuthServiceClient { get; }
        public IMechSyncServiceClient SyncServiceClient { get; }

        public ReviewableFilesTreeView DrawingExplorer { get; private set; }

        public LocalReview Review { get; }

        public FileMetadata DrawingMetadata { get; set; }
        public ReviewTarget ReviewTarget { get; set; }

        public string TempDownloadedDrawingFile { get; set; }
        public string TempDownloadedMarkupFile { get; set; }
        public string TempUploadedMarkupFile { get; set; }

        public string[] StatusesHavingMarkupFile { get; } = new string[] 
        {
            "Reviewing",
            "Rejected",
            "Fixed"
        };

        public string[] StatusesHavingReviewControlsEnabled { get; } = new string[] 
        {
            "Pending",
            "Reviewing",
            "Fixed"
        };

        public DrawingReviewer(
                IAuthenticationServiceClient authService,
                IMechSyncServiceClient mechService,
                IDrawingReviewerUI ui,
                LocalReview review,
                ILogger logger
            )
        {
            // validate inputs and assign properties
            AuthServiceClient = authService ?? throw new ArgumentNullException(nameof(authService));
            SyncServiceClient = mechService ?? throw new ArgumentNullException(nameof(mechService));
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            Review = review ?? throw new ArgumentNullException(nameof(review));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InitializeUiAsync()
        {
            DrawingExplorer = new ReviewableDrawingsTreeView(SyncServiceClient, Review, logger);
            DrawingExplorer.AttachTreeView(UI.DeltaDrawingsTreeView);
            DrawingExplorer.OnOpenReviewTarget += DrawingsExplorer_OnOpenReviewTarget;

            var designerDetails = await AuthServiceClient.GetUserDetailsAsync(Review.RemoteVersion.Owner.UserId);

            UI.HideDrawingMarkupPanel();
            UI.SetHeaderText($"Reviewing {Review}");
            UI.SetDesignerText(designerDetails.FullName);

            UI.ZoomButton.Click += ZoomButton_Click;
            UI.PanButton.Click += PanButton_Click;
            UI.ChangeRequestButton.Click += ChangeRequestButton_Click;
            UI.SaveProgressButton.Click += SaveProgressButton_Click;
            UI.CloseDrawingButton.Click += CloseDrawingButton_Click;
            UI.ZoomToAreaButton.Click += ZoomToAreaButton_Click;
            UI.ApproveDrawingButton.Click += ApproveDrawingButton_Click;
            UI.RejectDrawingButton.Click += RejectDrawingButton_Click;
            UI.RefreshReviewTargetsButton.Click += RefreshReviewTargetsButton_Click;

            await RefreshReviewTargetsAsync();
        }

        public async void DrawingReviewerControl_OpenDocError(string FileName, int ErrorCode, string ErrorString)
        {
            MessageBox.Show($"Failed to open drawing {FileName}: Error {ErrorCode}, {ErrorString}");
            await CloseReviewTargetAsync();
        }

        public async Task OpenReviewTargetAsync(ReviewTarget reviewTarget)
        {
            await new OpenDrawingReviewCommand(this, reviewTarget).RunAsync();
        }

        public async Task CloseReviewTargetAsync()
        {
            await new CloseDrawingReviewCommand(this).RunAsync();
        }

        public async Task ApproveReviewTargetAsync()
        {
            await new ApproveDrawingCommand(this, logger).RunAsync();
        }

        public async Task RejectReviewTargetAsync()
        {
            await new RejectDrawingCommand(this, logger).RunAsync();
        }

        public async Task RefreshReviewTargetsAsync()
        {
            await DrawingExplorer.RefreshAsync();
        }

        public async Task SaveReviewProgressAsync()
        {
            await new SaveDrawingReviewProgressCommand(this).RunAsync();
        }

        public async Task DownloadMarkupFileAsync()
        {
            await new DrawingMarkupFileDownloader(this).DownloadAsync();
        }

        public async Task<bool> UploadMarkupFileAsync()
        {
            return await new DrawingMarkupFileUploader(this).UploadAsync();
        }


        private async void ApproveDrawingButton_Click(object sender, EventArgs e)
        {
            await ApproveReviewTargetAsync();
        }

        private async void RejectDrawingButton_Click(object sender, EventArgs e)
        {
            await RejectReviewTargetAsync();
        }

        private async void SaveProgressButton_Click(object sender, EventArgs e)
        {
            await SaveReviewProgressAsync();
        }

        private void ZoomToAreaButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetZoomToAreaOperator();
        }

        private async void CloseDrawingButton_Click(object sender, EventArgs e)
        {
            await CloseReviewTargetAsync();
        }

        private void ChangeRequestButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetCloudWithLeaderMarkupOperator();
        }

        private async void RefreshReviewTargetsButton_Click(object sender, EventArgs e)
        {
            await DrawingExplorer.FetchReviewableFileMetadata();
        }

        private void PanButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetPanOperator();
            UI.DrawingReviewerControl.HostControl?.FindForm().Focus();
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetZoomOperator();
        }

        private async void DrawingsExplorer_OnOpenReviewTarget(object sender, OpenReviewTargetEventArgs e)
        {
            await OpenReviewTargetAsync(e.ReviewTarget);
        }
    }
}
