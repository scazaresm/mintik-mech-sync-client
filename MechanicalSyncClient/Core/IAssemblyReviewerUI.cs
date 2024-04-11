using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Core
{
    public interface IAssemblyReviewerUI
    {
        TreeView DeltaAssembliesTreeView { get; set; }

        Label HeaderLabel { get; set; }
        Label DesignerLabel { get; set; }
        ToolStripButton CloseAssemblyButton { get; set; }
        ToolStrip ReviewToolStrip { get; set; }
        ToolStripLabel StatusLabel { get; set; }
        ToolStripLabel ReviewTargetStatus { get; set; }
        TextBox ChangeRequestInput { get; set; }
        DataGridView ChangeRequestsGrid { get; set; }
        ToolStripButton ApproveAssemblyButton { get; set; }
        ToolStripButton RejectAssemblyButton { get; set; }
        ToolStripButton RefreshReviewTargetsButton { get; set; }
        SplitContainer ChangeRequestSplit { get; set; }

        void AddChangeRequestToGrid(ChangeRequest changeRequest);
        void HideReviewPanel();
        void PopulateChangeRequestGrid(ReviewTarget target);
        void SetDesignerText(string designerText);
        void SetHeaderText(string headerText);
        void SetReviewTargetStatusText(string status);
        void ShowReviewPanel();
    }
}
