using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher;
using Serilog;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DeliverablePublishingForm : Form
    {
        private readonly IVersionSynchronizer synchronizer;
        private readonly ISolidWorksStarter solidWorksStarter;
        private readonly ILogger logger;
        private DeliverablePublisher publisher;

        public DeliverablePublishingForm(IVersionSynchronizer synchronizer,
                                         ISolidWorksStarter solidWorksStarter,
                                         ILogger logger)
        {
            InitializeComponent();
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.solidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private async void DeliverablePublishingForm_Load(object sender, EventArgs e)
        {
            try
            {
                var ui = new DeliverablePublisherUI()
                {
                    DrawingsGridView = DrawingsGridView,
                    ViewBlockersButton = ViewBlockersButton,
                    StatusLabel = StatusLabel,
                    Progress = Progress,
                    MainToolStrip = MainToolStrip,
                    MainStatusStrip = MainStatusStrip,
                    PublishSelectedButton = PublishSelectedButton,
                    CancelSelectedButton = CancelSelectedButton,
                    ValidateButton = ValidateButton
                };
                publisher = new DeliverablePublisher(synchronizer, solidWorksStarter, ui, logger);
                publisher.InitializeUI();
                await publisher.ValidateDrawingsAsync();
            }
            catch(Exception ex)
            {
                var message = $"Failed to publish deliverables: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void ShowErrorDetailsButton_Click(object sender, EventArgs e)
        {
            if (DrawingsGridView.SelectedRows.Count != 1) return;

            var selectedRow = DrawingsGridView.SelectedRows[0];

            MessageBox.Show(selectedRow.Cells[3].Value.ToString(), "Publishing error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DrawingContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowErrorDetailsButton.Enabled = DrawingsGridView.SelectedRows.Count == 1 && 
                DrawingsGridView.SelectedRows[0].Cells[3].Value.ToString().ToLower().Contains("error");
        }
    }
}
