using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Core
{
    public interface ILocalFileListViewControl : IDisposable
    {
        ListView AttachedListView { get; }
        DictionaryListViewAdapter FileLookup { get; }

        void AttachListView(ListView listView);
        void AddCreatedFile(string filePath);
        void RemoveDeletedFile(string filePath);
        void SetSyncingStatusToFile(string filePath);
        void SetSyncedStatusToFile(string filePath);
        void SetErrorStatusToFile(string filePath);
        void PopulateFiles();
    }
}
