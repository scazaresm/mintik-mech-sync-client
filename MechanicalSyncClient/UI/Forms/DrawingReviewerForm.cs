using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Reviews.DrawingReviewer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                PanButton = PanButton,
                ChangeRequestButton = ChangeRequestButton,
                CloseDrawingButton = CloseDrawingButton,
                SaveProgressButton = SaveProgressButton,
            };
            drawingReviewer = new DrawingReviewer(syncServiceClient, ui, review);
            drawingReviewer.InitializeUI();
            _ = drawingReviewer.RefreshDeltaDrawings();
        }

        private void DrawingReviewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VersionSynchronizerForm.Instance.Show();
        }

        private void DrawingReviewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            drawingReviewer.CloseReviewTarget();
        }
    }
}
