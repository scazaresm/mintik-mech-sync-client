using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher.States;
using MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies;
using MechanicalSyncApp.Sync;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public class DeliverablePublisher : IDeliverablePublisher
    {
        private DeliverablePublisherState state;

        public string BasePublishingDirectory { get; set; } = @"Z:\MANUFACTURING\";

        public DeliverablePublisherUI UI { get; }
        public ISolidWorksStarter SolidWorksStarter { get; }
        public IVersionSynchronizer Synchronizer { get; } 

        private readonly ILogger logger;


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

        public async Task AnalyzeDeliverablesAsync()
        {
            var drawingFetcher = new ReviewableFileMetadataFetcher(Synchronizer, logger);

            var drawingRevisionValidationStrategy = new DrawingRevisionValidationStrategy(
                new NextDrawingRevisionCalculator(GetProjectPublishingDirectory(), logger),
                new DrawingRevisionRetriever(SolidWorksStarter, logger),
                logger
            );

            var customPropertiesValidationStrategy = new CustomPropertiesValidationStrategy(
                new ModelPropertiesRetriever(SolidWorksStarter, logger),
                logger
            );

            var drawingValidator = new DrawingValidator(
              drawingRevisionValidationStrategy,
              customPropertiesValidationStrategy,
              logger
            );

            SetState(new ValidateDrawingsState(drawingFetcher, drawingValidator, logger));
            await RunStepAsync();
        }

        public async Task PublishAsync(List<FileMetadata> validDrawings)
        {
            var modelExporter = new SolidWorksModelExporter(SolidWorksStarter, logger);
            var publishStrategy = new PublishDeliverablesStrategy(
                modelExporter, GetProjectPublishingDirectory(), logger
            );

            SetState(new PublishDeliverablesState(validDrawings, publishStrategy, logger));
            await RunStepAsync();
        }

        public async Task CancelPublishAsync(List<FileMetadata> toBeCancelled)
        {

        }

        public string GetProjectPublishingDirectory()
        {
            var projectFolderName = Synchronizer.Version.RemoteProject.FolderName;
            return Path.Combine(BasePublishingDirectory, DateTime.Now.Year.ToString(), projectFolderName);
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

            var allSelectedReadyForPublishing = selectedRows
                .Cast<DataGridViewRow>()
                .Where((row) => row.Cells["PublishingStatus"].Value.ToString() == "Ready")
                .Count() == selectedRows.Count;

            UI.PublishSelectedButton.Enabled = allSelectedReadyForPublishing;

            var allSelectedPublished = selectedRows
                .Cast<DataGridViewRow>()
                .Where((row) => row.Cells["PublishingStatus"].Value.ToString() == "Published")
                .Count() == selectedRows.Count;

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

        #endregion
    }
}
