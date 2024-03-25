using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer
{
    public class AssemblyReviewerUI: IAssemblyReviewerUI
    {
        public TreeView DeltaAssembliesTreeView { get; set; }
        
        public Label HeaderLabel {  get; set; }
        public Label DesignerLabel { get; set; }
        public SplitContainer MainSplit { get; set; }

        public ToolStripButton CloseAssemblyButton { get; set; }

        public ToolStrip ReviewToolStrip { get; set; }

        public void SetHeaderText(string headerText)
        {
            HeaderLabel.Text = headerText;
        }

        public void SetDesignerText(string designerText)
        {
            DesignerLabel.Text = designerText;
        }

        public void HideReviewPanel()
        {
            MainSplit.Panel1Collapsed = false;
            MainSplit.Panel2Collapsed = true;
        }

        public void ShowReviewPanel()
        {
            MainSplit.Panel1Collapsed = true;
            MainSplit.Panel2Collapsed = false;
        }
    }
}
