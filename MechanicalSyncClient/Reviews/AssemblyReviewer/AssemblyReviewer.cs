using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core;
using MechanicalSyncApp.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Reviews.AssemblyReviewer.Commands;
using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Reviews.DrawingReviewer.Commands;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer
{
    public class AssemblyReviewer : IAssemblyReviewer
    {
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

            Args.UI.HideReviewPanel();
            Args.UI.SetHeaderText($"Reviewing {Args.Review}");
            Args.UI.SetDesignerText(designerDetails.FullName);

            Args.UI.CloseAssemblyButton.Click += CloseAssemblyButton_Click;

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

        public async Task RefreshReviewTargetsAsync()
        {
            await AssembliesExplorer.RefreshAsync();
        }

        private async void CloseAssemblyButton_Click(object sender, EventArgs e)
        {
            await CloseReviewTargetAsync();
        }

        private async void AssembliesTreeView_OnOpenReviewTarget(object sender, OpenReviewTargetEventArgs e)
        {
            await OpenReviewTargetAsync(e.ReviewTarget);
        }

    }
}