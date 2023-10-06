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
    public partial class DesignViewerForm : Form
    {
        private readonly string filePath;

        public DesignViewerForm(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        private void DesignViewerForm_Load(object sender, EventArgs e)
        {
            var designViewer = new DesignViewer(filePath);
            Controls.Add(designViewer.HostControl);
            Text = Path.GetFileName(filePath);
        }
    }
}
