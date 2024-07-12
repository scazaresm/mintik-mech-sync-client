using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class IgnoreDrawingsForm : Form
    {
        private readonly IReviewableFileMetadataFetcher fileMetadataFetcher;
        private readonly IVersionSynchronizer versionSynchronizer;

        public IgnoreDrawingsForm(
                IReviewableFileMetadataFetcher fileMetadataFetcher,
                IVersionSynchronizer versionSynchronizer
            )
        {
            InitializeComponent();
            this.fileMetadataFetcher = fileMetadataFetcher ?? throw new ArgumentNullException(nameof(fileMetadataFetcher));
            this.versionSynchronizer = versionSynchronizer ?? throw new ArgumentNullException(nameof(versionSynchronizer));
        }

        private async void IgnoreDrawingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                var allDrawings = await fileMetadataFetcher.FetchReviewableDrawingsAsync();

                IgnoredDrawingsGrid.Rows.Clear();
                var remoteVersion = versionSynchronizer.Version.RemoteVersion;

                foreach (var drawing in allDrawings)
                {
                    var drawingFileName = Path.GetFileName(drawing.RelativeFilePath);
                    bool checkedState = remoteVersion.IgnoreDrawings.Contains(drawingFileName);

                    IgnoredDrawingsGrid.Rows.Add(checkedState, drawingFileName);
                }
                var fileNameColumn = IgnoredDrawingsGrid.Columns[1];
                IgnoredDrawingsGrid.Sort(fileNameColumn, ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to retrieve the list of ignored drawings, please try again later: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Close();
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new IgnoreDrawingsRequest()
                {
                    VersionId = versionSynchronizer.Version.RemoteVersion.Id
                };

                foreach (DataGridViewRow row in IgnoredDrawingsGrid.Rows)
                {
                    var checkedState = (bool)(row.Cells[0].Value);
                    var drawing = row.Cells[1].Value.ToString();

                    if (checkedState)
                        request.ToIgnore.Add(drawing);
                    else
                        request.ToStopIgnoring.Add(drawing);
                }
                versionSynchronizer.Version.RemoteVersion =
                    await versionSynchronizer.SyncServiceClient.IgnoreDrawingsAsync(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save ignored drawings, please try again later: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                Close();
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredDrawingsGrid.Rows)
                row.Cells[0].Value = true;
        }

        private void UnselectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredDrawingsGrid.Rows)
                row.Cells[0].Value = false;
        }
    }
}
