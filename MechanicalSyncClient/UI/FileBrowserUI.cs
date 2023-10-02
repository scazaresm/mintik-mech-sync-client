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
    public class FileBrowserUI
    {
        private readonly ListView listView;
        private readonly ImageList iconList;

        private string localPath;

        public DictionaryListViewAdapter FileLookup { get; private set; }

        public FileBrowserUI(ListView listView)
        {
            this.listView = listView ?? throw new ArgumentNullException(nameof(listView));
            iconList = new ImageList();
            iconList.ImageSize = new Size(42, 42);
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            FileLookup = new DictionaryListViewAdapter(listView.Items);
        }

        public void Navigate(string localPath)
        {
            if(!Directory.Exists(localPath))
            {
                throw new Exception($"Directory does not exist: {localPath}");
            }

            FileLookup.Clear();
            foreach (string localFile in Directory.GetFiles(localPath))
            {
                FileInfo fileInfo = new FileInfo(localFile);
                if (fileInfo.Name.StartsWith("~$"))
                    continue;

                FileLookup.Add(fileInfo.FullName, buildDefaultListViewItem(fileInfo));
            }
            listView.SmallImageList = iconList;
            this.localPath = localPath;
        }

        private ListViewItem buildDefaultListViewItem(FileInfo fileInfo)
        {
            iconList.Images.Add(Icon.ExtractAssociatedIcon(fileInfo.FullName));
            var listViewItem = new ListViewItem(fileInfo.Name, iconList.Images.Count - 1);
            listViewItem.StateImageIndex = 0;
            listViewItem.SubItems.Add("Synced");
            return listViewItem;
        }

        public void SetSyncingStatus(string localPath)
        {
            if (FileLookup.ContainsKey(localPath))
            {
                ListViewItem fileListViewItem = FileLookup[localPath];
                fileListViewItem.StateImageIndex = (int)FileSyncStatusIconIndex.Syncing;
                fileListViewItem.SubItems[1].Text = "Syncing...";
            }
        }

        public void SetSyncedStatus(string localPath)
        {
            if (FileLookup.ContainsKey(localPath))
            {
                ListViewItem fileListViewItem = FileLookup[localPath];
                fileListViewItem.StateImageIndex = (int)FileSyncStatusIconIndex.Synced;
                fileListViewItem.SubItems[1].Text = "Synced";
            }
        }

        public void Refresh()
        {
            Navigate(localPath);
        }
    }

}
