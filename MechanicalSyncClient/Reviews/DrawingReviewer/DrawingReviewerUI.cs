using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eDrawings.Interop.EModelViewControl;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Domain;
using System.Drawing;

namespace MechanicalSyncApp.Reviews.DrawingReviewer
{
    public class DrawingReviewerUI : IDrawingReviewerUI
    {

        public ToolStripButton ZoomButton { get; set; }

        public ToolStripButton ZoomToAreaButton { get; set; }

        public ToolStripButton PanButton {  get; set; }

        public ToolStripButton ChangeRequestButton { get; set; }

        public ToolStripButton SaveProgressButton { get; set; }

        public ToolStripButton CloseDrawingButton { get; set; }

        public ToolStripButton ApproveDrawingButton { get; set; }

        public ToolStripButton RejectDrawingButton { get; set; }

        public SplitContainer MainSplit { get; set; }

        public Panel MarkupPanel { get; set; }

        public ToolStripProgressBar DownloadProgressBar { get; set; }

        public ToolStripStatusLabel MarkupStatus { get; set; }

        public ToolStripLabel ReviewTargetStatus { get; set; }

        public TreeView DeltaDrawingsTreeView { get; set; }

        public Label HeaderLabel { get; set; }

        public Label DesignerLabel { get; set; }

        public DrawingReviewerControl DrawingReviewerControl { get; set; }

        public void SetReviewControlsEnabled(bool enabled)
        {
            ApproveDrawingButton.Enabled = enabled;
            RejectDrawingButton.Enabled = enabled;
            ChangeRequestButton.Enabled = enabled;
            SaveProgressButton.Enabled = enabled;
        }

        public void SetReviewTargetStatusText(string status)
        {
            ReviewTargetStatus.Text = status;

            ReviewTargetStatus.BackgroundImageLayout = ImageLayout.Stretch;
            ReviewTargetStatus.BackgroundImage = new Bitmap(1, 1);
            var g = Graphics.FromImage(ReviewTargetStatus.BackgroundImage);

            switch (status)
            {
                case "Pending":
                default:
                    g.Clear(Color.Black);
                    ReviewTargetStatus.ForeColor = Color.White;
                    break;

                case "Reviewing":
                    g.Clear(Color.Black);
                    ReviewTargetStatus.ForeColor = Color.Yellow;
                    break;

                case "Approved":
                    g.Clear(Color.DarkGreen);
                    ReviewTargetStatus.ForeColor = Color.White;
                    break;

                case "Rejected":
                    g.Clear(Color.DarkRed);
                    ReviewTargetStatus.ForeColor = Color.White;
                    break;

                case "Fixed":
                    g.Clear(Color.Black);
                    ReviewTargetStatus.ForeColor = Color.Aqua;
                    break;
            }
        }

        public void ShowDrawingMarkupPanel()
        {
            MainSplit.Panel1Collapsed = true;
            MainSplit.Panel2Collapsed = false;
        }

        public void HideDrawingMarkupPanel()
        {
            MainSplit.Panel1Collapsed = false;
            MainSplit.Panel2Collapsed = true;
        }

        public void ReportDownloadProgress(int progress)
        {
            if (DownloadProgressBar.GetCurrentParent().InvokeRequired)
            {
                DownloadProgressBar.GetCurrentParent().Invoke(
                    new Action<int>(ReportDownloadProgress),
                    progress
                );
            }
            else
            {
                DownloadProgressBar.Value = progress;
            }
        }

        public void ShowDownloadProgress()
        {
            DownloadProgressBar.Visible = true;
        }

        public void HideDownloadProgress()
        {
            DownloadProgressBar.Visible = false;
        }

        public void SetMarkupStatusText(string statusText)
        {
            MarkupStatus.Text = statusText;
        }

        public void SetHeaderText(string headerText)
        {
            HeaderLabel.Text = headerText;
        }

        public void SetDesignerText(string designerText)
        {
            DesignerLabel.Text = designerText;
        }

        public void LoadDrawing(string localDrawingPath, 
                                _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocumentEventHandler)
        {
            if (DrawingReviewerControl != null)
            {
                MarkupPanel.Controls.Remove(DrawingReviewerControl.HostControl);
                DrawingReviewerControl?.Dispose();
            }

            DrawingReviewerControl = new DrawingReviewerControl(localDrawingPath)
            {
                OnFailedLoadingDocument = onFailedLoadingDocumentEventHandler
            };
            MarkupPanel.Controls.Add(DrawingReviewerControl.HostControl);
        }

        public void DisposeDrawing()
        {
            DrawingReviewerControl?.Dispose();
        }

    }
}
