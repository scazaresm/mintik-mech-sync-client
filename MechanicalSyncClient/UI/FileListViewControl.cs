using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.UI.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class FileListViewControl : IFileListViewControl
    {
        private string localDirectory;
        private readonly string extensionFilter;
        private readonly IVersionChangeMonitor changeMonitor;
        private bool disposedValue;

        public ListView AttachedListView { get; private set; }

        public DictionaryListViewAdapter FileLookup { get; private set; }

        public FileListViewControl(string localDirectory, string extensionFilter, IVersionChangeMonitor changeMonitor)
        {
            this.localDirectory = localDirectory ?? throw new ArgumentNullException(nameof(localDirectory));
            this.extensionFilter = extensionFilter ?? throw new ArgumentNullException(nameof(extensionFilter));
            this.changeMonitor = changeMonitor ?? throw new ArgumentNullException(nameof(changeMonitor));
            AttachListView(new ListView());
        }

        public void AttachListView(ListView listView)
        {
            AttachedListView = listView;
            AttachedListView.MouseDoubleClick += AttachedListView_MouseDoubleClick;
            FileLookup = new DictionaryListViewAdapter(listView.Items);
            PopulateFiles();
        }

        public void AddCreatedFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (!FileLookup.ContainsKey(filePath))
                FileLookup.Add(filePath, BuildDefaultListViewItem(filePath));

            if (changeMonitor.IsMonitoring())
                SetSyncingStatusToFile(filePath);
            else
                SetOfflineStatusToFile(filePath);
        }

        public void RemoveDeletedFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (FileLookup.ContainsKey(filePath))
            {
                FileLookup.Remove(filePath);
            }
        }

        public void SetSyncingStatusToFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (FileLookup.ContainsKey(filePath))
            {
                ListViewItem fileListViewItem = FileLookup[filePath];
                fileListViewItem.StateImageIndex = (int)FileSyncStatusIconIndex.Syncing;
                if (fileListViewItem.SubItems.Count == 1)
                {
                    fileListViewItem.SubItems.Add("Syncing...");
                    fileListViewItem.SubItems.Add(filePath);
                }
                else
                {
                    fileListViewItem.SubItems[1].Text = "Syncing...";
                }
            }
        }

        public void SetSyncedStatusToFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (FileLookup.ContainsKey(filePath))
            {
                ListViewItem fileListViewItem = FileLookup[filePath];
                fileListViewItem.StateImageIndex = (int)FileSyncStatusIconIndex.Synced;
                if (fileListViewItem.SubItems.Count == 1)
                {
                    fileListViewItem.SubItems.Add("Synced");
                    fileListViewItem.SubItems.Add(filePath);
                }
                else
                {
                    fileListViewItem.SubItems[1].Text = "Synced";
                }
            }
        }

        public void SetOfflineStatusToFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (FileLookup.ContainsKey(filePath))
            {
                ListViewItem fileListViewItem = FileLookup[filePath];
                fileListViewItem.StateImageIndex = -1;
                if (fileListViewItem.SubItems.Count == 1)
                {
                    fileListViewItem.SubItems.Add("Offline");
                    fileListViewItem.SubItems.Add(filePath);
                }
                else
                {
                    fileListViewItem.SubItems[1].Text = "Offline";
                }
            }
        }

        public void SetErrorStatusToFile(string filePath)
        {
            filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
            if (FileLookup.ContainsKey(filePath))
            {
                ListViewItem fileListViewItem = FileLookup[filePath];
                fileListViewItem.StateImageIndex = (int)FileSyncStatusIconIndex.Error;
                if (fileListViewItem.SubItems.Count == 1)
                {
                    fileListViewItem.SubItems.Add("Error");
                    fileListViewItem.SubItems.Add(filePath);
                }
                else
                {
                    fileListViewItem.SubItems[2].Text = "Error";
                }
            }
        }

        public void PopulateFiles()
        {
            if (!Directory.Exists(localDirectory))
            {
                throw new Exception($"Directory does not exist: {localDirectory}");
            }

            FileLookup.Clear();
            AttachedListView.Enabled = false;

            var allLocalFiles = Directory.GetFiles(localDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string localFile in allLocalFiles)
            {
                // skip lock files
                if (Path.GetFileName(localFile).StartsWith("~$"))
                    continue;

                // skip extensions not allowed by the extension filter
                var fileExtension = Path.GetExtension(localFile);
                if (!extensionFilter.ToLower().Contains(fileExtension.ToLower()))
                    continue;

                FileLookup.Add(localFile, BuildDefaultListViewItem(localFile));

                if (changeMonitor.IsMonitoring())
                    SetSyncedStatusToFile(localFile);
                else
                    SetOfflineStatusToFile(localFile);
            }
            AttachedListView.Enabled = true;
        }

        private ListViewItem BuildDefaultListViewItem(string filePath)
        {
            var relativeFilePath = filePath.Replace(localDirectory + Path.DirectorySeparatorChar, "");

            var listViewItem = new ListViewItem(relativeFilePath);

            var groupIndex = GetListViewGroupIndex(filePath);
            if(groupIndex >= 0)
                listViewItem.Group = AttachedListView.Groups[groupIndex];
            return listViewItem;
        }

        private int GetListViewGroupIndex(string filePath)
        {
            if(AttachedListView.Groups.Count != 3)
            {
                return -1;
            }
            switch(Path.GetExtension(filePath).ToLower())
            {
                case ".sldasm": return 0;
                case ".sldprt": return 1;
                case ".slddrw": return 2;
                default: return -1;
            }
        }

        private void AttachedListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected ListViewItem, if any
            ListViewItem selectedItem = AttachedListView.SelectedItems.Count > 0 ? AttachedListView.SelectedItems[0] : null;

            // Check if a ListViewItem was double-clicked
            if (selectedItem != null)
            {
                string filePath = selectedItem.SubItems[2].Text;
                var designViewerForm = new DesignFileViewerForm(filePath);
                designViewerForm.Show();
            }
        }

        private void RemoveEventListeners()
        {
            AttachedListView.MouseDoubleClick -= AttachedListView_MouseDoubleClick;
        }

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    RemoveEventListeners();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
