using MechanicalSyncApp.Core;
using MechanicalSyncApp.UI;
using System;
using System.Threading.Tasks;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Reviews.AssemblyReviewer.Commands;
using MechanicalSyncApp.Core.Args;
using System.Windows.Forms;
using System.Drawing;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer
{
    public class AssemblyReviewer : IAssemblyReviewer
    {
        private const string CreateChangeRequestPrompt = "Type here to create a new change request...";

        public AssemblyReviewerArgs Args { get; }
        public ReviewTarget ReviewTarget { get; set; }
        public FileMetadata AssemblyMetadata { get; set; }

        public ReviewableFilesTreeView AssembliesExplorer { get; private set; }

        public AssemblyReviewer(AssemblyReviewerArgs args)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Args.Validate();
        }

        public async Task InitializeUiAsync()
        {
            AssembliesExplorer = new ReviewableAssembliesTreeView(Args.SyncServiceClient, Args.Review, Args.Logger);
            AssembliesExplorer.AttachTreeView(Args.UI.DeltaAssembliesTreeView);
            AssembliesExplorer.OnOpenReviewTarget += AssembliesTreeView_OnOpenReviewTarget;

            var designerDetails = await Args.AuthServiceClient.GetUserDetailsAsync(Args.Review.RemoteVersion.Owner.UserId);

            var ui = Args.UI;

            ui.HideReviewPanel();
            ui.SetHeaderText($"Reviewing {Args.Review}");
            ui.SetDesignerText(designerDetails.FullName);

            ui.ChangeRequestInput.Text = CreateChangeRequestPrompt;
            ui.CloseAssemblyButton.Click += CloseAssemblyButton_Click;
            ui.ChangeRequestInput.Enter += ChangeRequestInput_Enter;
            ui.ChangeRequestInput.Leave += ChangeRequestInput_Leave;
            ui.ChangeRequestInput.KeyDown += ChangeRequestInput_KeyDown;
            ui.ChangeRequestsGrid.CellDoubleClick += ChangeRequestsGrid_CellDoubleClick;

            await RefreshReviewTargetsAsync();
        }

     

        public async Task OpenReviewTargetAsync(ReviewTarget reviewTarget)
        {
            await new OpenAssemblyReviewCommand(this, reviewTarget, Args.Logger).RunAsync();
        }

        public async Task CloseReviewTargetAsync()
        {
            await new CloseAssemblyReviewCommand(this, Args.Logger).RunAsync();
        }

        public async Task CreateChangeRequestAsync()
        {
            await new CreateChangeRequestCommand(this, Args.Logger).RunAsync();
        }

        public async Task ViewChangeRequestAsync(ChangeRequest changeRequest)
        {
            await new ViewEditChangeRequestCommand(this, changeRequest, Args.Logger).RunAsync();
        }

        public async Task RefreshReviewTargetsAsync()
        {
            await AssembliesExplorer.RefreshAsync();
        }

        private async void CloseAssemblyButton_Click(object sender, EventArgs e)
        {
            await CloseReviewTargetAsync();
        }

        private void ChangeRequestInput_Enter(object sender, EventArgs e)
        {
            var input = (sender as TextBox);

            // prepare to receive change request description
            if (input.Text.Trim() == CreateChangeRequestPrompt)
            {
                input.Text = string.Empty;
                input.ForeColor = Color.Black;
            }
        }

        private void ChangeRequestInput_Leave(object sender, EventArgs e)
        {
            var input = (sender as TextBox);

            // prompt user to start typing to create a new change request
            if (input.Text.Trim() == string.Empty)
            {
                input.Text = CreateChangeRequestPrompt;
                input.ForeColor = Color.Silver;
            }
        }

        private async void ChangeRequestInput_KeyDown(object sender, KeyEventArgs e)
        {
            var input = (sender as TextBox);

            if (e.KeyCode == Keys.Enter && !e.Shift && input.Text.Trim() != string.Empty)
                await CreateChangeRequestAsync();
        }

        private async void ChangeRequestsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRowsCollection = (sender as DataGridView).SelectedRows;

            if (selectedRowsCollection == null || selectedRowsCollection.Count == 0)
                return;

            var selectedChangeRequest = (selectedRowsCollection[0].Tag as ChangeRequest);
            await ViewChangeRequestAsync(selectedChangeRequest);
        }

        private async void AssembliesTreeView_OnOpenReviewTarget(object sender, OpenReviewTargetEventArgs e)
        {
            await OpenReviewTargetAsync(e.ReviewTarget);
        }
    }
}