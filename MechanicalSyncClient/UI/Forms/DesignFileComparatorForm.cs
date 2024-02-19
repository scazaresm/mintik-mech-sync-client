using Serilog;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DesignFileComparatorForm : Form
    {
        private readonly string leftHandFilePath;
        private readonly string rightHandFilePath;

        private DesignFileViewerControl leftHandFileViewer;
        private DesignFileViewerControl rightHandFileViewer;

        public string LeftHandDescription
        {
            get
            {
                return LeftHandTitle.Text;
            }
            set
            {
                LeftHandTitle.Text = value;
                if (!LeftHandTitle.Visible) LeftHandTitle.Visible = true;
            }
        }

        public string RightHandDescription
        {
            get
            {
                return RightHandTitle.Text;
            }
            set
            {
                RightHandTitle.Text = value;
                if (!RightHandTitle.Visible) RightHandTitle.Visible = true;
            }
        }


        public DesignFileComparatorForm(string leftHandFilePath, string rightHandFilePath)
        {
            Log.Debug("Creating DesignFileComparatorForm instance...");
            InitializeComponent();
            this.leftHandFilePath = leftHandFilePath;
            this.rightHandFilePath = rightHandFilePath;
            Log.Debug("Successfully created DesignFileComparatorForm.");
        }

        public void Initialize(string leftHandFileTitle, string rightHandFileTitle)
        {
            Log.Debug("Initializing DesignFileComparatorForm...");

            if (leftHandFilePath != null)
            {
                leftHandFileViewer = new DesignFileViewerControl(leftHandFilePath)
                {
                    OnFailedLoadingDocument = FileViewer_OpenDocError
                };
                leftHandFileViewer.HostControl.Dock = DockStyle.Fill;
                MainContainer.Panel1.Controls.Add(leftHandFileViewer.HostControl);
            }
            else
            {
                LeftHandNotFound.Visible = true;
                LeftHandToolStrip.Enabled = false;
            }

            LeftHandTitle.Text = leftHandFileTitle;
            if (!LeftHandTitle.Visible) LeftHandTitle.Visible = true;

            if (rightHandFilePath != null)
            {
                rightHandFileViewer = new DesignFileViewerControl(rightHandFilePath)
                {
                    OnFailedLoadingDocument = FileViewer_OpenDocError
                };
                rightHandFileViewer.HostControl.Dock = DockStyle.Fill;
                MainContainer.Panel2.Controls.Add(rightHandFileViewer.HostControl);
            }
            else
            {
                RightHandNotFound.Visible = true;
                RightHandToolStrip.Enabled = false;
            }

            RightHandTitle.Text = rightHandFileTitle;
            if (!RightHandTitle.Visible) RightHandTitle.Visible = true;

            Log.Debug("Successfully initialized DesignFileComparatorForm.");
        }

        private void FileViewer_OpenDocError(string FileName, int ErrorCode, string ErrorString)
        {
            Log.Debug($"An error occurred while opening {FileName} in eDrawings File Viewer: error code {ErrorCode}, {ErrorString}");
            CloseForm();
        }

        public void CloseForm()
        {
            Log.Debug($"Closing DesignFileComparatorForm.");
            if (InvokeRequired)
                Invoke(new Action(CloseForm));
            else
                Close();
        }


        #region Left hand toolbar

        private void LeftMeasureButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetMeasureOperator();
        }

        private void LeftRotateButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetRotateOperator();
        }

        private void LeftPanButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetPanOperator();
        }

        private void LeftSelectButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetSelectOperator();
        }

        private void LeftZoomToAreaButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetZoomToAreaOperator();
        }

        private void LeftZoomButton_Click(object sender, EventArgs e)
        {
            leftHandFileViewer.SetZoomOperator();
        }

        #endregion

        #region Right hand toolbar

        private void RightZoomButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetZoomOperator();
        }

        private void RightZoomToAreaButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetZoomToAreaOperator();
        }

        private void RightSelectButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetSelectOperator();
        }

        private void RightPanButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetPanOperator();
        }

        private void RightRotateButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetRotateOperator();
        }
        private void RightMeasureButton_Click(object sender, EventArgs e)
        {
            rightHandFileViewer.SetMeasureOperator();
        }

        #endregion


    }
}
