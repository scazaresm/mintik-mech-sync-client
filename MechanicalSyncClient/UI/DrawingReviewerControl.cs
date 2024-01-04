using eDrawings.Interop.EModelMarkupControl;
using eDrawings.Interop.EModelViewControl;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class DrawingReviewerControl
    {
        private readonly string filePath;

        public EModelViewControl ModelViewControl { get; private set; }

        public EModelMarkupControl ModelMarkupControl { get; private set; }

        public EDrawingsHost HostControl { get; private set; }

        public _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler OnFailedLoadingDocument { get; set; }

        public DrawingReviewerControl(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            HostControl = new EDrawingsHost();
            HostControl.ControlLoaded += OnControlLoaded;
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            ModelViewControl = ctrl;
            ModelMarkupControl = ModelViewControl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl");

            if (OnFailedLoadingDocument != null)
            {
                ModelViewControl.OnFailedLoadingDocument += OnFailedLoadingDocument;
            }
            ModelViewControl.OpenDoc(filePath, false, false, false, "");
            HostControl.Dock = DockStyle.Fill;
        }

        public void SetTextWithLeaderMarkupOperator()
        {
            ModelMarkupControl.ViewOperator = EMVMarkupOperators.eMVOperatorMarkupTextWithLeader;
        }

        public void SetCloudWithLeaderMarkupOperator()
        {
            ModelMarkupControl.ViewOperator = EMVMarkupOperators.eMVOperatorMarkupCloudWithLeader;
        }

        public void SetPanOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorPan;
        }

        public void SetZoomOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorZoom;
        }

        public void SetZoomToAreaOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorZoomToArea;
        }

        public void SaveMarkupFile(string filePath)
        {
            ModelMarkupControl.ShowSaveMarkup(filePath, false);
        }

        public void OpenMarkupFile(string filePath)
        {
            ModelViewControl.OpenMarkupFile(filePath);
        }

        #region Dispose pattern
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                HostControl.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
