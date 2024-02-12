using eDrawings.Interop.EModelMarkupControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DesignFileViewerForm : Form
    {
        private readonly string filePath;
        private readonly string fileName;
        private readonly string versionFolder;
        private DesignFileViewerControl designViewerControl;




        public DesignFileViewerForm(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public DesignFileViewerForm(string filePath, string fileName, string versionFolder)
        {
            InitializeComponent();
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this.fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            this.versionFolder = versionFolder ?? throw new ArgumentNullException(nameof(versionFolder));
        }

        private void DesignViewerForm_Load(object sender, EventArgs e)
        {
         
        }

        public void Initialize()
        {
            designViewerControl = new DesignFileViewerControl(filePath)
            {
                OnFailedLoadingDocument = DesignViewerControl_OpenDocError
            };
            designViewerControl.HostControl.Dock = DockStyle.Fill;

            ViewerPanel.Controls.Add(designViewerControl.HostControl);

            // caller can provide custom file name at constructor, otherwise the actual file name will be taken
            if (fileName != null)
                Text = fileName;
            else
                Text = Path.GetFileName(filePath);

            VersionRelatedLabel.Visible = false;
            if (versionFolder != null)
            {
                VersionRelatedLabel.BackColor = Color.Black;
                switch (versionFolder)
                {
                    case "Latest":
                        VersionRelatedLabel.Text = "This design file belongs to a Latest version and can be safely used as reference.";
                        VersionRelatedLabel.ForeColor = Color.Green;
                        break;

                    case "Ongoing":
                        VersionRelatedLabel.Text = "WARNING: This design file belongs to an Ongoing version and shall NOT be used as reference.";
                        VersionRelatedLabel.ForeColor = Color.Red;
                        break;
                }
                VersionRelatedLabel.Visible = true;
            }
        }

        private void DesignViewerControl_OpenDocError(string FileName, int ErrorCode, string ErrorString)
        {
            CloseForm();
        }

        public void CloseForm()
        {
            if (InvokeRequired)
                Invoke(new Action(CloseForm));
            else
                Close();
        }

        private void MeasureButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetMeasureOperator();
        }

        private void PanButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetPanOperator();
        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetRotateOperator();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetSelectOperator();
        }

        private void ZoomToAreaButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetZoomToAreaOperator();
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            designViewerControl.SetZoomOperator();
        }
    }
}
