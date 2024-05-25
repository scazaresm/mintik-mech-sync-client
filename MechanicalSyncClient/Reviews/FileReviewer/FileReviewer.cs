using MechanicalSyncApp.Core;
using MechanicalSyncApp.UI;
using System;
using System.Threading.Tasks;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Args;
using System.Windows.Forms;
using System.Drawing;
using MechanicalSyncApp.Reviews.FileReviewer.Commands;

namespace MechanicalSyncApp.Reviews.FileReviewer
{
    public class FileReviewer : IFileReviewer
    {
        private const string CreateChangeRequestPrompt = "Type here to create a new change request...";

        public FileReviewerArgs Args { get; }
        public ReviewTarget ReviewTarget { get; set; }
        public FileMetadata Metadata { get; set; }

        public ReviewableFilesTreeView FileExplorer { get; private set; }

        public FileReviewer(FileReviewerArgs args)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Args.Validate();
        }

        public async Task InitializeUiAsync()
        {
            var remoteReview = Args.Review.RemoteReview;

            if (remoteReview.TargetType == "AssemblyFile")
                FileExplorer = new ReviewableAssembliesTreeView(Args.SyncServiceClient, Args.Review, Args.Logger);
            else if (remoteReview.TargetType == "DrawingFile")
                FileExplorer = new ReviewableDrawingsTreeView(Args.SyncServiceClient, Args.Review, Args.Logger);
            else
                throw new Exception($"Unsupported TargetType in review: {remoteReview.TargetType}");

            FileExplorer.AttachTreeView(Args.UI.DeltaFilesTreeView);
            FileExplorer.OnOpenReviewTarget += AssembliesTreeView_OnOpenReviewTarget;

            var designerDetails = await Args.AuthServiceClient.GetUserDetailsAsync(Args.Review.RemoteVersion.Owner.UserId);

            var ui = Args.UI;

            ui.HideReviewPanel();
            ui.SetHeaderText($"Reviewing {Args.Review}");
            ui.SetDesignerText(designerDetails.FullName);

            ui.ChangeRequestInput.Text = CreateChangeRequestPrompt;
            ui.CloseFileReviewButton.Click += CloseFileReviewButton_Click;
            ui.ChangeRequestInput.Enter += ChangeRequestInput_Enter;
            ui.ChangeRequestInput.Leave += ChangeRequestInput_Leave;
            ui.ChangeRequestInput.KeyDown += ChangeRequestInput_KeyDown;
            ui.ChangeRequestsGrid.CellDoubleClick += ChangeRequestsGrid_CellDoubleClick;
            ui.ApproveFileButton.Click += ApproveFileButton_Click;
            ui.RefreshReviewTargetsButton.Click += RefreshReviewTargetsButton_Click;
            ui.RejectFileButton.Click += RejectFileButton_Click;

            var parentForm = ui.MainSplit.ParentForm;
            parentForm.FormClosing += ParentForm_FormClosing;

            await RefreshReviewTargetsAsync();
        }


        public async Task OpenReviewTargetAsync(ReviewTarget reviewTarget)
        {
            await new OpenFileReviewCommand(this, reviewTarget, Args.Logger).RunAsync();
        }

        public async Task CloseReviewTargetAsync()
        {
            await new CloseFileReviewCommand(this, Args.Logger).RunAsync();
        }

        public async Task CreateChangeRequestAsync()
        {
            await new CreateChangeRequestCommand(this, Args.Logger).RunAsync();
        }

        public async Task ViewChangeRequestAsync(ChangeRequest changeRequest)
        {
            await new ViewChangeRequestCommand(this, changeRequest, Args.Logger).RunAsync();
        }

        public async Task ApproveFileAsync()
        {
            await new ApproveFileCommand(this, Args.Logger).RunAsync();
        }

        public async Task RejectFileAsync()
        {
            await new RejectFileCommand(this, Args.Logger).RunAsync();
        }

        public async Task RefreshReviewTargetsAsync()
        {
            await FileExplorer.RefreshAsync();
        }

        private async void CloseFileReviewButton_Click(object sender, EventArgs e)
        {
            await CloseReviewTargetAsync();
        }

        private void ChangeRequestInput_Enter(object sender, EventArgs e)
        {
            var input = sender as TextBox;

            // prepare to receive change request description
            if (input.Text.Trim() == CreateChangeRequestPrompt)
            {
                input.Text = string.Empty;
                input.ForeColor = Color.Black;
            }
        }

        private void ChangeRequestInput_Leave(object sender, EventArgs e)
        {
            var input = sender as TextBox;

            // prompt user to start typing to create a new change request
            if (input.Text.Trim() == string.Empty)
            {
                input.Text = CreateChangeRequestPrompt;
                input.ForeColor = Color.Silver;
            }
        }

        private async void ChangeRequestInput_KeyDown(object sender, KeyEventArgs e)
        {
            var input = sender as TextBox;

            if (e.KeyCode == Keys.Enter && !e.Shift && input.Text.Trim() != string.Empty)
            {
                await CreateChangeRequestAsync();
            }
        }

        private async void ChangeRequestsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRowsCollection = (sender as DataGridView).SelectedRows;

            if (selectedRowsCollection == null || selectedRowsCollection.Count == 0)
                return;

            var selectedChangeRequest = selectedRowsCollection[0].Tag as ChangeRequest;
            await ViewChangeRequestAsync(selectedChangeRequest);
        }

        private async void RefreshReviewTargetsButton_Click(object sender, EventArgs e)
        {
            await RefreshReviewTargetsAsync();
        }

        private async void AssembliesTreeView_OnOpenReviewTarget(object sender, OpenReviewTargetEventArgs e)
        {
            await OpenReviewTargetAsync(e.ReviewTarget);
        }

        private async void ApproveFileButton_Click(object sender, EventArgs e)
        {
            await ApproveFileAsync();
        }
        private async void RejectFileButton_Click(object sender, EventArgs e)
        {
            await RejectFileAsync();
        }

        private async void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var logger = Args.Logger;
            try
            {
                logger.Debug($"ParentForm_FormClosing starts...");

                var mainSplit = Args.UI.MainSplit;
                var isReviewingFile = mainSplit.Panel1Collapsed == true && mainSplit.Panel2Collapsed == false;

                if (isReviewingFile)
                    await CloseReviewTargetAsync();

                Args.SolidWorksStarter?.Dispose();
                Args.SolidWorksStarter = null;
                logger.Debug($"Successfully handled ParentForm_FormClosing.");
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to handle ParentForm_FormClosing event: {ex.Message}");
            }
        }
    }
}