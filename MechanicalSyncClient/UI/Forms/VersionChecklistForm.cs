using MechanicalSyncApp.Core;
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
    public partial class VersionChecklistForm : Form
    {
        // key is FileMetadata Id, value is the actual FileMetadata object for assemblies and drawings 
        private readonly Dictionary<string, FileMetadata> ReviewableAssemblyIndex = new Dictionary<string, FileMetadata>();
        private readonly Dictionary<string, FileMetadata> ReviewableDrawingsIndex = new Dictionary<string, FileMetadata>();

        // key is a FileMetadata Id, value is the count of ReviewTarget with Approved status in server
        private readonly Dictionary<string, int> FileApprovalCounts = new Dictionary<string, int>();


        private int approvedDrawingsCount = 0;
        private int approvedAssembliesCount = 0;


        public IVersionSynchronizer Synchronizer { get; }

        public VersionChecklistForm(IVersionSynchronizer synchronizer)
        {
            InitializeComponent();
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            _ = RefreshChecklist();
        }

        private async Task RefreshChecklist()
        {
            await FetchReviewableFiles();
            await FetchFileApprovalCounts();
            PopulateReviewableDrawings();
        }

        private void PopulateReviewableDrawings()
        {
            ReviewableDrawingsListView.Items.Clear();
            approvedDrawingsCount = 0;
            foreach (var drawing in ReviewableDrawingsIndex.Values)
            {
                var item = new ListViewItem(drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));
                item.UseItemStyleForSubItems = false;

                var approvalCount = FileApprovalCounts.ContainsKey(drawing.Id)
                    ? FileApprovalCounts[drawing.Id] : 0;

                if (approvalCount > 0)
                    approvedDrawingsCount++;

                var status = GetStatusText(approvalCount);

                var approvalCountSubitem = item.SubItems.Add(approvalCount.ToString());
                approvalCountSubitem.ForeColor = approvalCount == 0 ? Color.Red : Color.Green;
                item.SubItems.Add(status);
                ReviewableDrawingsListView.Items.Add(item);
            }
            drawingVerificationTab.ImageIndex = IsDrawingsComplete() ? 0 : 1;
        }

        private async Task FetchReviewableFiles()
        {
            try
            {
                var reviewableDrawings = await new ReviewableFileMetadataFetcher(
                    Synchronizer.SyncServiceClient,
                    Synchronizer.Version.RemoteVersion.Id
                ).FetchReviewableDrawingsAsync();
                    
                foreach (var drawing in reviewableDrawings)
                {
                    if (!ReviewableDrawingsIndex.ContainsKey(drawing.Id))
                        ReviewableDrawingsIndex.Add(drawing.Id, drawing);
                    else
                        ReviewableDrawingsIndex[drawing.Id] = drawing;
                }

                var reviewableAssemblies = await new ReviewableFileMetadataFetcher(
                    Synchronizer.SyncServiceClient,
                    Synchronizer.Version.RemoteVersion.Id
                ).FetchReviewableAssembliesAsync();

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

            }
        }

        private async Task FetchFileApprovalCounts()
        {
            try
            {
                var reviews = await Synchronizer.SyncServiceClient.GetVersionReviewsAsync(Synchronizer.Version.RemoteVersion.Id);

                FileApprovalCounts.Clear();
                foreach(var review in reviews)
                {
                    foreach(var target in review.Targets)
                    {
                        if (target.Status != "Approved") continue;

                        if (!FileApprovalCounts.ContainsKey(target.TargetId))
                            FileApprovalCounts[target.TargetId] = 1;
                        else
                            FileApprovalCounts[target.TargetId]++;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private string GetStatusText(int approvalCount)
        {
            var status = approvalCount > 0
               ? "Ready"
               : "Needs at least 1 approval";
            return status;
        }

        private bool IsDrawingsComplete()
        {
            return approvedDrawingsCount == ReviewableDrawingsIndex.Count;
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await RefreshChecklist();
        }
    }
}
