using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class FileViewer
    {
        private readonly ImageList iconList;
        public ListView AttachedListView { get; private set; }
        private string localDirectory;

        public DictionaryListViewAdapter FileLookup { get; private set; }

        public FileViewer(string localDirectory)
        {
            this.localDirectory = localDirectory ?? throw new ArgumentNullException(nameof(localDirectory));
            AttachedListView = new ListView();
            iconList = new ImageList();
            iconList.ImageSize = new Size(32, 32);
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            FileLookup = new DictionaryListViewAdapter(AttachedListView.Items);
        }

        public void AttachListView(ListView listView)
        {
            this.AttachedListView = listView;
            FileLookup = new DictionaryListViewAdapter(listView.Items);
            PopulateFiles();
        }

        public void AddCreatedFile(string filePath)
        {
            if (!FileLookup.ContainsKey(filePath))
                FileLookup.Add(filePath, BuildDefaultListViewItem(filePath));
            SetSyncingStatusToFile(filePath);
        }

        public void RemoveDeletedFile(string filePath)
        {
            if (FileLookup.ContainsKey(filePath))
            {
                FileLookup.Remove(filePath);
            }
        }

        public void SetSyncingStatusToFile(string filePath)
        {
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

        public void SetErrorStatusToFile(string filePath)
        {
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

            iconList.Images.Clear();
            FileLookup.Clear();
            AttachedListView.Enabled = false;
            //listView.SmallImageList = iconList;

            var allLocalFiles = Directory.GetFiles(localDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string localFile in allLocalFiles)
            {
                if (Path.GetFileName(localFile).StartsWith("~$"))
                    continue;

                FileLookup.Add(localFile, BuildDefaultListViewItem(localFile));
                SetSyncedStatusToFile(localFile);
            }
            AttachedListView.Enabled = true;
        }

        private ListViewItem BuildDefaultListViewItem(string filePath)
        {
            var relativeFilePath = filePath.Replace(localDirectory + Path.DirectorySeparatorChar, "");

            //iconList.Images.Add(Icon.ExtractAssociatedIcon(filePath));
            //var listViewItem = new ListViewItem(relativeFilePath, iconList.Images.Count - 1);
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
            switch(Path.GetExtension(filePath))
            {
                case ".sldasm": return 0;
                case ".sldprt": return 1;
                case ".slddrw": return 2;
                default: return -1;
            }
        }
    }

}
