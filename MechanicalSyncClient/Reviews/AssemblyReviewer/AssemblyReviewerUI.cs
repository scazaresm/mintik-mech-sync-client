using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public SplitContainer ChangeRequestSplit { get; set; }

        public ToolStripButton CloseAssemblyButton { get; set; }

        public ToolStripButton ApproveAssemblyButton { get; set; }

        public ToolStripButton RejectAssemblyButton { get; set; }

        public ToolStripButton RefreshReviewTargetsButton { get; set; }

        public ToolStripLabel StatusLabel { get; set; }

        public ToolStripLabel ReviewTargetStatus { get; set; }

        public TextBox ChangeRequestInput { get; set; }

        public ToolStrip ReviewToolStrip { get; set; }

        public DataGridView ChangeRequestsGrid { get; set; }


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

        public void PopulateChangeRequestGrid(ReviewTarget target)
        {
            ChangeRequestsGrid.Rows.Clear();
            foreach (var changeRequest in target.ChangeRequests)
                AddChangeRequestToGrid(changeRequest);
        }

        public void AddChangeRequestToGrid(ChangeRequest changeRequest)
        {
            var row = new DataGridViewRow();
            row.CreateCells(
                ChangeRequestsGrid,
                changeRequest.ChangeDescription,
                changeRequest.Status
            );
            row.Tag = changeRequest;

            ChangeRequestsGrid.Rows.Add(row);
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
    }
}
