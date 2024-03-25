using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class ReviewableDrawingsGrid
    {
        public DictionaryDataGridViewAdapter DrawingLookup { get; set; }

        public DataGridView AttachedDataGridView { get; set; }

        public void AttachDataGridView(DataGridView dataGridView)
        {
            AttachedDataGridView = dataGridView ?? throw new ArgumentNullException(nameof(dataGridView));
            DrawingLookup = new DictionaryDataGridViewAdapter(dataGridView.Rows);
        }

        public void AddDrawing(FileMetadata drawing, string defaultPublishingStatus)
        {
            var drawingIcon = GetDrawingIcon(drawing);
            var row = new DataGridViewRow();
            row.CreateCells(AttachedDataGridView, 
                drawingIcon, 
                drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar), 
                drawing.ApprovalCount,
                defaultPublishingStatus
            );
            row.Tag = drawing;
            DrawingLookup.Add(drawing.Id, row);
        }

        public Image GetDrawingIcon(FileMetadata drawing)
        {
            return Properties.Resources.file_icon_16;
        }
    }
}
