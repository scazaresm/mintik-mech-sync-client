using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Reviews.DrawingReviewer;
using System.Drawing;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DrawingReviewerForm : Form
    {

        private readonly IDrawingReviewer drawingReviewer;

        public DrawingReviewerForm(IMechSyncServiceClient syncServiceClient, LocalReview review)
        {
            InitializeComponent();

            IDrawingReviewerUI ui = new DrawingReviewerUI()
            {
                DownloadProgressBar = DownloadProgressBar,
                HeaderLabel = HeaderLabel,
                MainSplit = MainSplit,
                MarkupPanel = MarkupPanel,
                MarkupStatus = MarkupStatus,
                DeltaDrawingsTreeView = DeltaDrawingsTreeView,
                ZoomButton = ZoomButton,
                ZoomToAreaButton = ZoomToAreaButton,
                PanButton = PanButton,
                ApproveDrawingButton = ApproveDrawingButton,
                RejectDrawingButton = RejectDrawingButton,
                ChangeRequestButton = ChangeRequestButton,
                CloseDrawingButton = CloseDrawingButton,
                SaveProgressButton = SaveProgressButton,
                ReviewTargetStatus = ReviewTargetStatus,
            };
            drawingReviewer = new DrawingReviewer(syncServiceClient, ui, review);
            drawingReviewer.InitializeUI();
            _ = drawingReviewer.RefreshDeltaTargetsAsync();
        }

        private void DrawingReviewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VersionSynchronizerForm.Instance.Show();
        }

        private void DrawingReviewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            drawingReviewer.CloseReviewTargetAsync();
        }

        private void DrawingReviewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.Control && e.KeyCode == Keys.S)
                drawingReviewer.UI.SaveProgressButton.PerformClick();
            else if (e.Control && e.KeyCode == Keys.A)
                drawingReviewer.UI.ChangeRequestButton.PerformClick();
        }
    }
}
