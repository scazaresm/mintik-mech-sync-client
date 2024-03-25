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

        void HideReviewPanel();
        void SetDesignerText(string designerText);
        void SetHeaderText(string headerText);
        void ShowReviewPanel();
    }
}
