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
        ToolStripButton PanButton { get; }
        ToolStripButton ChangeRequestButton { get; }

        ToolStripButton SaveProgressButton { get; }

        ToolStripButton CloseDrawingButton { get; }

        SplitContainer MainSplit { get; }

        Panel MarkupPanel { get; }

        ToolStripProgressBar DownloadProgressBar { get; }

        ToolStripStatusLabel MarkupStatus { get; }

        Label HeaderLabel { get; set; }

        TreeView DeltaDrawingsTreeView { get; }

        DrawingReviewerControl DrawingReviewerControl { get; }

        void HideDrawingMarkupPanel();

        void ShowDrawingMarkupPanel();

        void ShowDownloadProgress();

        void HideDownloadProgress();

        void ReportDownloadProgress(int progress);

        void SetMarkupStatusText(string status);

        void SetHeaderText(string headerText);

        void LoadDrawing(string localDrawingPath,
                         _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocumentEventHandler);

        void DisposeDrawing();
    }
}
