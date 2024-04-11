using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eDrawings.Interop.EModelViewControl;
using MechanicalSyncApp.UI;

namespace MechanicalSyncApp.Core
{
    public interface IDrawingReviewerUI
    {
        ToolStripButton ZoomButton { get; }
        ToolStripButton ZoomToAreaButton { get; }
        ToolStripButton PanButton { get; }
        ToolStripButton ChangeRequestButton { get; }
        ToolStripButton SaveProgressButton { get; }
        ToolStripButton CloseDrawingButton { get; }
        ToolStripButton ApproveDrawingButton { get; }
        ToolStripButton RejectDrawingButton { get; }

        SplitContainer MainSplit { get; }
        Panel MarkupPanel { get; }
        ToolStripProgressBar DownloadProgressBar { get; }
        ToolStripStatusLabel MarkupStatus { get; }
        ToolStripLabel ReviewTargetStatus { get; }
        ToolStripButton RefreshReviewTargetsButton { get; set; }
        Label DesignerLabel { get; set; }
        Label HeaderLabel { get; set; }
        TreeView DeltaDrawingsTreeView { get; }
        DrawingReviewerControl DrawingReviewerControl { get; }

        void SetReviewControlsEnabled(bool enabled);

        void SetReviewTargetStatusText(string status);

        void HideDrawingMarkupPanel();

        void ShowDrawingMarkupPanel();

        void ShowDownloadProgress();

        void HideDownloadProgress();

        void ReportDownloadProgress(int progress);

        void SetMarkupStatusText(string status);

        void SetHeaderText(string headerText);

        void SetDesignerText(string designerText);

        void LoadDrawing(string localDrawingPath,
                         _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocumentEventHandler);
        
        void LoadDrawing(string localDrawingPath,
                         string localMarkupPath,
                      _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocumentEventHandler);


        void DisposeDrawing();
    }
}
