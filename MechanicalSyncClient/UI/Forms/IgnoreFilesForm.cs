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
    public partial class IgnoreFilesForm : Form
    {
        private readonly IReviewableFileMetadataFetcher fileMetadataFetcher;
        private readonly IVersionSynchronizer versionSynchronizer;

        public IgnoreFilesForm(
                IReviewableFileMetadataFetcher fileMetadataFetcher,
                IVersionSynchronizer versionSynchronizer
            )
        {
            InitializeComponent();
            this.fileMetadataFetcher = fileMetadataFetcher ?? throw new ArgumentNullException(nameof(fileMetadataFetcher));
            this.versionSynchronizer = versionSynchronizer ?? throw new ArgumentNullException(nameof(versionSynchronizer));
        }

        private async void IgnoreFilesForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadIgnoredDrawingsAsync();
                await LoadIgnoredAssembliesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to retrieve the list of ignored files, please try again later: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Close();
            }
        }

        private async Task LoadIgnoredDrawingsAsync()
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

        private async Task LoadIgnoredAssembliesAsync()
        {
            var allAssemblies = await fileMetadataFetcher.FetchReviewableAssembliesAsync();

            IgnoredAssembliesGrid.Rows.Clear();
            var remoteVersion = versionSynchronizer.Version.RemoteVersion;

            foreach (var assy in allAssemblies)
            {
                var assyFileName = Path.GetFileName(assy.RelativeFilePath);
                bool checkedState = remoteVersion.IgnoreAssemblies.Contains(assyFileName);

                IgnoredAssembliesGrid.Rows.Add(checkedState, assyFileName);
            }
            var fileNameColumn = IgnoredAssembliesGrid.Columns[1];
            IgnoredAssembliesGrid.Sort(fileNameColumn, ListSortDirection.Ascending);
        }


        private async Task MakeIgnoreDrawingsRequestAsync()
        {
            var request = new IgnoreFilesRequest()
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

        private async Task MakeIgnoreAssembliesRequestAsync()
        {
            var request = new IgnoreFilesRequest()
            {
                VersionId = versionSynchronizer.Version.RemoteVersion.Id
            };

            foreach (DataGridViewRow row in IgnoredAssembliesGrid.Rows)
            {
                var checkedState = (bool)(row.Cells[0].Value);
                var assembly = row.Cells[1].Value.ToString();

                if (checkedState)
                    request.ToIgnore.Add(assembly);
                else
                    request.ToStopIgnoring.Add(assembly);
            }
            versionSynchronizer.Version.RemoteVersion =
                await versionSynchronizer.SyncServiceClient.IgnoreAssembliesAsync(request);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                await MakeIgnoreDrawingsRequestAsync();
                await MakeIgnoreAssembliesRequestAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save ignored files, please try again later: {ex.Message}",
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

        private void SelectAllDrawingsButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredDrawingsGrid.Rows)
                row.Cells[0].Value = true;
        }

        private void UnselectAllDrawingsButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredDrawingsGrid.Rows)
                row.Cells[0].Value = false;
        }

        private void SelectAllAssembliesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredAssembliesGrid.Rows)
                row.Cells[0].Value = true;
        }

        private void UnselectAllAssembliesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in IgnoredAssembliesGrid.Rows)
                row.Cells[0].Value = false;
        }
    }
}
