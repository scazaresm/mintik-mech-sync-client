using eDrawings.Interop.EModelMarkupControl;
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
    public partial class DesignFileViewerForm : Form
    {
        private readonly string filePath;
        private DesignFileViewerControl designViewerControl;

        public DesignFileViewerForm(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        private void DesignViewerForm_Load(object sender, EventArgs e)
        {
            designViewerControl = new DesignFileViewerControl(filePath, DesignViewerControl_OpenDocError);
            ViewerPanel.Controls.Add(designViewerControl.HostControl);
            Text = Path.GetFileName(filePath);
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
