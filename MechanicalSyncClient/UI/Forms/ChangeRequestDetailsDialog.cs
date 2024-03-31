using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ChangeRequestDetailsDialog : Form
    {
        public ChangeRequest ChangeRequest { get; }

        private bool editMode = false;

        public ChangeRequestDetailsDialog(string changeDescription)
        {
            InitializeComponent();
            editMode = false;
            ChangeRequest = new ChangeRequest()
            {
                ChangeDescription = changeDescription
            };
            ChangeDescription.ReadOnly = false;
            PasteImageButton.Visible = true;
            EditButton.Visible = false;
            DeleteButton.Visible = false;
            RejectChangeButton.Visible = false;
            AcceptChangeButton.Visible = false;
            OkButton.Text = "Save";

        }

        public ChangeRequestDetailsDialog(ChangeRequest changeRequest)
        {
            InitializeComponent();
            editMode = true;
            ChangeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));

            if (ChangeRequest.Parent == null)
                throw new NullReferenceException("ChangeRequest.Parent cannot be null.");

            // is editable only when the parent review target has not been approved or rejected
            EditButton.Visible = IsChangeRequestEditable(changeRequest);
            DeleteButton.Visible = IsChangeRequestEditable(changeRequest);

            // is reviewable only when the change is already implemented and the parent review target has 'Fixed' status
            RejectChangeButton.Visible = IsChangeRequestReviewable(changeRequest);
            AcceptChangeButton.Visible = IsChangeRequestReviewable(changeRequest);

            PasteImageButton.Visible = false;  // will be made visible on edit
            ChangeDescription.ReadOnly = true; // will be made read-write on edit
            OkButton.Text = "Close";           // will be changed to 'Save' on edit
        }

        private void ChangeRequestDetailsDialog_Load(object sender, EventArgs e)
        {
            ChangeDescription.Text = ChangeRequest.ChangeDescription;

            if (!editMode)
                TakePictureFromClipboard();
            else
                DetailsPicture.Image = ChangeRequest.DetailsPicture;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void TakePictureFromClipboard()
        {
            if (!Clipboard.ContainsImage())
            {
                ChangeRequest.DetailsPicture = null;
                return;
            }

            var image = Clipboard.GetImage();

            if (image != null)
            {
                ChangeRequest.DetailsPicture = image;
                DetailsPicture.Image = image;
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
            TakePictureFromClipboard();
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private bool IsChangeRequestEditable(ChangeRequest changeRequest)
        {
            var editableStatuses = new string[]
            {
                ReviewTargetStatus.Pending.ToString(),
                ReviewTargetStatus.Reviewing.ToString()
            };
            return editableStatuses.Contains(changeRequest.Parent.Status);
        }

        private bool IsChangeRequestReviewable(ChangeRequest changeRequest)
        {
            return changeRequest.Status == ChangeRequestStatus.Done.ToString() &&
                changeRequest.Parent.Status == ReviewTargetStatus.Fixed.ToString();
        }
    }
}
