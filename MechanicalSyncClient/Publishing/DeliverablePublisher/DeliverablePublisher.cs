using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher.States;
using MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies;
using MechanicalSyncApp.Sync;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public class DeliverablePublisher : IDeliverablePublisher, IDisposable
    {
        private const string READY_STATUS = "Ready";
        private const string PUBLISHED_STATUS = "Published";

        private DeliverablePublisherState state;

        public DeliverablePublisherUI UI { get; }
        public ISolidWorksStarter SolidWorksStarter { get; }
        public IVersionSynchronizer Synchronizer { get; } 

        private readonly ILogger logger;

        private SyncGlobalConfig globalConfig;
        private bool disposedValue;

        private CancellationTokenSource ValidationCancellationToken;

        public DeliverablePublisher(IVersionSynchronizer synchronizer, 
                                    ISolidWorksStarter solidWorksStarter,
                                    DeliverablePublisherUI ui, 
                                    ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            SolidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void InitializeUI()
        {
            UI.Initialize();
            UI.DrawingsGridView.SelectionChanged += DrawingsGridView_SelectionChanged;
            UI.ViewBlockersButton.Click += ViewBlockersButton_Click;
            UI.PublishSelectedButton.Click += PublishSelectedButton_Click;
            UI.CancelSelectedButton.Click += CancelSelectedButton_Click;
            UI.ValidateButton.Click += ValidateButton_Click;
        }

       
        public void SetState(DeliverablePublisherState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetPublisher(this);
            this.state.UpdateUI();
        }

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
            else
                throw new InvalidOperationException("Trying to run a step before actually setting the current step.");
        }

        public async Task ValidateDrawingsAsync()
        {
            try
            {
                var drawingFetcher = new ReviewableFileMetadataFetcher(Synchronizer, logger);

                var drawingRevisionValidationStrategy = new DrawingRevisionValidationStrategy(
                    new NextDrawingRevisionCalculator(GetFullPublishingDirectory(), logger),
                    new DrawingRevisionRetriever(SolidWorksStarter, logger),
                    logger
                );

                if (globalConfig is null)
                    globalConfig = await Synchronizer.SyncServiceClient.GetGlobalConfigAsync();

                var customPropertiesValidationStrategy = new CustomPropertiesValidationStrategy(
                    new ModelPropertiesRetriever(SolidWorksStarter, logger),
                    globalConfig,
                    logger
                );

                var drawingValidator = new DrawingValidator(
                  drawingRevisionValidationStrategy,
                  customPropertiesValidationStrategy,
                  logger
                );

                ValidationCancellationToken?.Dispose();
                ValidationCancellationToken = new CancellationTokenSource();

                SetState(new ValidateDrawingsState(drawingFetcher, drawingValidator, logger)
                {
                    CancellationTokenSource = ValidationCancellationToken
                });
                await RunStepAsync();
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {

            }
        }

        public async Task PublishAsync(List<FileMetadata> validDrawings)
        {
            var response = MessageBox.Show(
                "Publish selected drawings?", 
                "Confirmation", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question
            );
            if (response != DialogResult.OK) return;

            var designerEmail = Synchronizer.AuthServiceClient.LoggedUserDetails.Email;
            var projectName = Synchronizer.Version.RemoteProject.FolderName;

            var modelExporter = new SolidWorksModelExporter(SolidWorksStarter, logger);

            var summaryFileDirectory = Path.Combine(
                Synchronizer.BasePublishingDirectory,
                Synchronizer.RelativePublishingSummaryDirectory
            );

            var publishStrategy = new PublishDeliverablesToFolderStrategy(
                new PublishDeliverablesToFolderStrategyArgs()
                {
                    SyncServiceClient = Synchronizer.SyncServiceClient,
                    ModelExporter = modelExporter,
                    FullPublishingDirectory = GetFullPublishingDirectory(),
                    RelativePublishingDirectory = GetRelativePublishingDirectory(),
                    DesignerEmail = designerEmail, 
                    Version = Synchronizer.Version,
                    SummaryFileDirectory = summaryFileDirectory,
                },
                logger
            );
            SetState(new PublishDeliverablesState(validDrawings, publishStrategy, logger));
            await RunStepAsync();
        }

        public async Task CancelPublishAsync(List<FileMetadata> toBeCancelled)
        {
            var response = MessageBox.Show(
                "Cancel publishings for selected drawings?",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );
            if (response != DialogResult.OK) return;

            var cancelStrategy = new CancelPublishingStrategy(Synchronizer.SyncServiceClient, logger);
            SetState(new CancelPublishingsState(cancelStrategy, toBeCancelled, logger));
            await RunStepAsync();
        }

        private string GetFullPublishingDirectory()
        {
            var basePublishingDirectory = Synchronizer.BasePublishingDirectory;
            return Path.Combine(basePublishingDirectory, GetRelativePublishingDirectory());
        }

        private string GetRelativePublishingDirectory()
        {
            var project = Synchronizer.Version.RemoteProject;
            var projectFolderName = project.FolderName;
            var projectYear = project.CreatedAt.Year.ToString();
            return Path.Combine(projectYear, projectFolderName);
        }

        #region UI Management

        private void DrawingsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRows = UI.DrawingsGridView.SelectedRows;

            if (selectedRows == null)
                return;

            UI.ViewBlockersButton.Enabled =
                selectedRows.Count == 1 &&
                (selectedRows[0].Tag as FileMetadata).PublishingStatus == PublishingStatus.Blocked;

            var allSelectedReadyForPublishing = selectedRows.Count > 0 && selectedRows
                .Cast<DataGridViewRow>()
                .Count((row) => row.Cells["PublishingStatus"].Value.ToString() == READY_STATUS) == selectedRows.Count;

            UI.PublishSelectedButton.Enabled = allSelectedReadyForPublishing;

            var allSelectedPublished = selectedRows.Count > 0 && selectedRows
                .Cast<DataGridViewRow>()
                .Count((row) => row.Cells["PublishingStatus"].Value.ToString() == PUBLISHED_STATUS) == selectedRows.Count;

            UI.CancelSelectedButton.Enabled = allSelectedPublished;
        }

        private void ViewBlockersButton_Click(object sender, EventArgs e)
        {
            var selectedRows = UI.DrawingsGridView.SelectedRows;

            if (selectedRows == null || selectedRows.Count != 1)
                return;

            var selectedDrawing = selectedRows[0].Tag as FileMetadata;
            new PublishingBlockersForm(selectedDrawing).ShowDialog();
        }

        private async void PublishSelectedButton_Click(object sender, EventArgs e)
        {
            var selectedRows = UI.DrawingsGridView.SelectedRows;

            if (selectedRows == null || selectedRows.Count < 1)
                return;

            var readyDrawings = selectedRows.Cast<DataGridViewRow>()
                .Where((row) => row.Cells["PublishingStatus"].Value.ToString() == "Ready")
                .Select((row) => row.Tag as FileMetadata)
                .ToList();

            await PublishAsync(readyDrawings);
        }

        private async void CancelSelectedButton_Click(object sender, EventArgs e)
        {
            var selectedRows = UI.DrawingsGridView.SelectedRows;

            if (selectedRows == null || selectedRows.Count < 1)
                return;

            var publishedDrawings = selectedRows.Cast<DataGridViewRow>()
                .Where((row) => row.Cells["PublishingStatus"].Value.ToString() == "Published")
                .Select((row) => row.Tag as FileMetadata)
                .ToList();

            await CancelPublishAsync(publishedDrawings);
        }

        private async void ValidateButton_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripButton).Text == "Cancel validation")
            {
                var confirmation = MessageBox.Show(
                    "Are you sure to cancel the drawing validation?",
                    "Cancel drawing validation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation == DialogResult.Yes)
                    ValidationCancellationToken.Cancel();

                return;
            }

            try
            {
                UI.StatusLabel.Text = "Looking for changes in local copy...";
                UI.ValidateButton.Enabled = false;

                var syncRemoteCommand = new SyncRemoteCommand(Synchronizer, logger)
                {
                    NotifyWhenComplete = false,
                    EnableToolStripWhenComplete = false
                };

                await syncRemoteCommand.RunAsync();

                if (!syncRemoteCommand.Complete)
                {
                    MessageBox.Show(
                        "Your local copy has changes and needs to be synced with the remote server before validating, please try again.",
                        "Local copy has changes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }
                await ValidateDrawingsAsync();
            }
            finally
            {
                UI.ValidateButton.Enabled = true;
            }
        }

        #endregion

        #region Dispose pattern

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ValidationCancellationToken?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
