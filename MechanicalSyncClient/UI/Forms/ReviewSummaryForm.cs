using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ReviewSummaryForm : Form
    {
        // key is FileMetadata Id, value is the actual FileMetadata object for assemblies and drawings 
        private readonly Dictionary<string, FileMetadata> ReviewableAssemblyIndex = new Dictionary<string, FileMetadata>();
        private readonly Dictionary<string, FileMetadata> ReviewableDrawingsIndex = new Dictionary<string, FileMetadata>();
        private readonly ILogger logger;
        private int approvedDrawingsCount = 0;
        private int approvedAssembliesCount = 0;

        public IVersionSynchronizer Synchronizer { get; }

        public ReviewSummaryForm(IVersionSynchronizer synchronizer, ILogger logger)
        {
            InitializeComponent();
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ = RefreshChecklistAsync();
        }

        private async Task RefreshChecklistAsync()
        {
            await FetchReviewableFiles();
            PopulateReviewableDrawings();
            PopulateReviewableAssemblies();
            NextStepButton.Enabled = IsDrawingsComplete() && IsAssemblyComplete();
        }

        private void PopulateReviewableDrawings()
        {
            ReviewableDrawingsListView.Items.Clear();
            approvedDrawingsCount = 0;
            foreach (var drawing in ReviewableDrawingsIndex.Values)
            {
                var item = new ListViewItem(drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.UseItemStyleForSubItems = false;

                if (drawing.ApprovalCount > 0)
                    approvedDrawingsCount++;

                var status = GetStatusText(drawing.ApprovalCount);

                var approvalCountSubitem = item.SubItems.Add(drawing.ApprovalCount.ToString());
                approvalCountSubitem.ForeColor = drawing.ApprovalCount == 0 ? Color.Red : Color.Green;
                item.SubItems.Add(status);
                item.Tag = drawing;
                ReviewableDrawingsListView.Items.Add(item);
            }
            drawingVerificationTab.ImageIndex = IsDrawingsComplete() ? 0 : 1;
        }

        private void PopulateReviewableAssemblies()
        {
            ReviewableAssembliesListView.Items.Clear();
            approvedAssembliesCount = 0;
            foreach (var assembly in ReviewableAssemblyIndex.Values)
            {
                var item = new ListViewItem(assembly.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.UseItemStyleForSubItems = false;
                
                if (assembly.ApprovalCount > 0)
                    approvedAssembliesCount++;

                var status = GetStatusText(assembly.ApprovalCount);

                var approvalCountSubitem = item.SubItems.Add(assembly.ApprovalCount.ToString());
                approvalCountSubitem.ForeColor = assembly.ApprovalCount == 0 ? Color.Red : Color.Green;
                item.SubItems.Add(status);
                item.Tag = assembly;
                ReviewableAssembliesListView.Items.Add(item);
            }
            assemblyVerificationTab.ImageIndex = IsAssemblyComplete() ? 0 : 1;
        }

        private async Task FetchReviewableFiles()
        {
            try
            {
                var fetcher = new ReviewableFileMetadataFetcher(Synchronizer, Log.Logger);

                var reviewableDrawings = await fetcher.FetchReviewableDrawingsAsync();

                var remoteVersion = Synchronizer.Version.RemoteVersion;

                // skip ignored drawings for this version
                reviewableDrawings = reviewableDrawings.Where((d) =>
                    !remoteVersion.IgnoreDrawings.Contains(Path.GetFileName(d.RelativeFilePath))
                ).ToList();

                foreach (var drawing in reviewableDrawings)
                {
                    if (!ReviewableDrawingsIndex.ContainsKey(drawing.Id))
                        ReviewableDrawingsIndex.Add(drawing.Id, drawing);
                    else
                        ReviewableDrawingsIndex[drawing.Id] = drawing;
                }

                var reviewableAssemblies = await fetcher.FetchReviewableAssembliesAsync();

                foreach (var assembly in reviewableAssemblies)
                {
                    if (!ReviewableAssemblyIndex.ContainsKey(assembly.Id))
                        ReviewableAssemblyIndex.Add(assembly.Id, assembly);
                    else
                        ReviewableAssemblyIndex[assembly.Id] = assembly;
                }
            }
            catch(Exception ex)
            {
                var message = $"Failed to fetch reviewable files: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStatusText(int approvalCount)
        {
            var status = approvalCount > 0 ? "Ready" : "Needs at least 1 approval";
            return status;
        }

        private bool IsDrawingsComplete()
        {
            return approvedDrawingsCount == ReviewableDrawingsIndex.Count;
        }

        private bool IsAssemblyComplete()
        {
            return approvedAssembliesCount == ReviewableAssemblyIndex.Count;
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await RefreshChecklistAsync();
        }

        private void PublishAllButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CancelArchivingButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
