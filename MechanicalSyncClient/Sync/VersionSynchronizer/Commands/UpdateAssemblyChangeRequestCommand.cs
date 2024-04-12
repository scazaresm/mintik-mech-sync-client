using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class UpdateAssemblyChangeRequestCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ChangeRequest changeRequest;
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; }

        public UpdateAssemblyChangeRequestCommand(
            IVersionSynchronizer synchronizer, 
            ChangeRequest changeRequest,
            ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.changeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            try
            {
                // show change request details and accept changes only if review target is rejected
                bool isReadOnly = Synchronizer.CurrentFileReviewTarget.Status != "Rejected";

                var dialog = new UpdateChangeRequestDialog(changeRequest, Synchronizer, logger)
                {
                    ReadOnly = isReadOnly
                };
                var result = dialog.ShowDialog();

                if (isReadOnly || result != DialogResult.OK) return;

                // update the change request in db
                await Synchronizer.SyncServiceClient.UpdateChangeRequestAsync(changeRequest.Id, new ChangeRequestUpdateableFields()
                {
                    DesignerComments = changeRequest.DesignerComments,
                    ChangeDescription = changeRequest.ChangeDescription,
                    Status = changeRequest.Status
                });

                // reflect changes on the grid
                var grid = Synchronizer.UI.FileChangeRequestGrid;

                var changeRequestRow = grid.Rows.Cast<DataGridViewRow>().FirstOrDefault((row) =>
                    (row.Tag as ChangeRequest).Id == changeRequest.Id
                );

                if (changeRequestRow != null)
                {
                    changeRequestRow.Cells[0].Value = changeRequest.ChangeDescription;
                    changeRequestRow.Cells[1].Value = changeRequest.Status;
                    changeRequestRow.Cells[2].Value = changeRequest.DesignerComments;
                }
            }
            catch(Exception ex)
            {
                var message = $"Could not open change request details: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
