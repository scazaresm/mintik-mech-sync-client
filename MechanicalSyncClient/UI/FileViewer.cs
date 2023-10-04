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
        public ListView ListView { get; private set; }
        private readonly ImageList iconList;

        private string localDirectory;

        public DictionaryListViewAdapter FileLookup { get; private set; }

        public FileViewer(ListView listView, string localDirectory)
        {
            this.localDirectory = localDirectory ?? throw new ArgumentNullException(nameof(localDirectory));
            ListView = listView ?? throw new ArgumentNullException(nameof(listView));
            iconList = new ImageList();
            iconList.ImageSize = new Size(32, 32);
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            FileLookup = new DictionaryListViewAdapter(listView.Items);
            PopulateFiles();
        }

        public void AddCreatedFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if(!FileLookup.ContainsKey(fileInfo.FullName))
                FileLookup.Add(fileInfo.FullName, BuildDefaultListViewItem(fileInfo));
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

            FileLookup.Clear();
            foreach (string localFile in Directory.GetFiles(localDirectory, "*.*", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(localFile);
                if (fileInfo.Name.StartsWith("~$"))
                    continue;

                FileLookup.Add(fileInfo.FullName, BuildDefaultListViewItem(fileInfo));
                SetSyncedStatusToFile(localFile);
            }
            ListView.SmallImageList = iconList;
        }

        private ListViewItem BuildDefaultListViewItem(FileInfo fileInfo)
        {
            var relativeFilePath = fileInfo.FullName.Replace(localDirectory + Path.DirectorySeparatorChar, "");

            iconList.Images.Add(Icon.ExtractAssociatedIcon(fileInfo.FullName));
            var listViewItem = new ListViewItem(relativeFilePath, iconList.Images.Count - 1);

            var groupIndex = GetListViewGroupIndex(fileInfo);
            if(groupIndex >= 0)
                listViewItem.Group = ListView.Groups[groupIndex];
            return listViewItem;
        }

        private int GetListViewGroupIndex(FileInfo fileInfo)
        {
            if(ListView.Groups.Count != 3)
            {
                return -1;
            }
            switch(fileInfo.Extension.ToLower())
            {
                case ".sldasm": return 0;
                case ".sldprt": return 1;
                case ".slddrw": return 2;
                default: return -1;
            }
        }
    }

}
