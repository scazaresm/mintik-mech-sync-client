using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public partial class PublishingBlockersForm : Form
    {
        private readonly FileMetadata drawing;

        public PublishingBlockersForm(FileMetadata drawing)
        {
            InitializeComponent();
            this.drawing = drawing ?? throw new ArgumentNullException(nameof(drawing));
        }

        private void PublishingBlockersForm_Load(object sender, EventArgs e)
        {
            var drawingFileName = Path.GetFileName(drawing.RelativeFilePath);
            Header.Text = $"The following issues were encountered in {drawingFileName}:";

            IssuesGrid.Rows.Clear();

            foreach(var issue in drawing.ValidationIssues)
            {
                IssuesGrid.Rows.Add(issue);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
