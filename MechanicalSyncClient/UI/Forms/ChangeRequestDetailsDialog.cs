using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ChangeRequestDetailsDialog : Form
    {
        public event EventHandler OnDelete;

        public ChangeRequest ChangeRequest { get; }

        private bool viewMode = false;

        private bool hasValidImage = false;

        public ChangeRequestDetailsDialog(string changeDescription)
        {
            InitializeComponent();
            viewMode = false;
            ChangeRequest = new ChangeRequest()
            {
                ChangeDescription = changeDescription
            };
            ChangeDescription.ReadOnly = false;
            PasteImageButton.Visible = true;
            DeleteButton.Visible = false;
            OkButton.Text = "Save";
            OkButton.Enabled = false;
        }

        public ChangeRequestDetailsDialog(ChangeRequest changeRequest)
        {
            InitializeComponent();
            viewMode = true;
            ChangeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));

            if (ChangeRequest.Parent == null)
                throw new NullReferenceException("ChangeRequest.Parent cannot be null.");

            // is deleteable only when the parent review target has not been approved or rejected
            DeleteButton.Visible = IsChangeRequestDeleteable(changeRequest);

            PasteImageButton.Visible = false; 
            ChangeDescription.ReadOnly = true;
            OkButton.Text = "Close"; 
            CancelActionButton.Visible = false;
        }

        private void ChangeRequestDetailsDialog_Load(object sender, EventArgs e)
        {
            if (!viewMode)
            {
                hasValidImage = Clipboard.ContainsImage();
                TakeImageFromClipboard();
            }
            else
            {
                DetailsPictureBox.Image = ChangeRequest.DetailsImage;
            }
            ChangeDescription.Text = ChangeRequest.ChangeDescription;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            OkButton.Enabled = false;
            DialogResult = DialogResult.OK;
        }
        
        private void TakeImageFromClipboard()
        {
            if (!Clipboard.ContainsImage())
            {
                ChangeRequest.DetailsImage = null;
                return;
            }

            var image = Clipboard.GetImage();

            if (image != null)
            {
                ChangeRequest.DetailsImage = image;
                DetailsPictureBox.Image = image;
            }
            Clipboard.Clear();
        }

        private void PasteImageButton_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
            {
                MessageBox.Show("There is no image on the clipboard.", "No image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TakeImageFromClipboard();

            hasValidImage = true;
            if (!OkButton.Enabled)
                OkButton.Enabled = ChangeDescription.Text.Length > 0;
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private bool IsChangeRequestDeleteable(ChangeRequest changeRequest)
        {
            var deleteableStatuses = new string[]
            {
                ReviewTargetStatus.Pending.ToString(),
                ReviewTargetStatus.Reviewing.ToString()
            };
            return deleteableStatuses.Contains(changeRequest.Parent.Status);
        }

        private void ChangeDescription_TextChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = OkButton.Text == "Close" || (hasValidImage && ChangeDescription.Text.Length > 0);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDelete?.Invoke(this, EventArgs.Empty);
        }
    }
}
