using eDrawings.Interop.EModelMarkupControl;
using eDrawings.Interop.EModelViewControl;
using Serilog;
using System;
using System.IO;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class DrawingReviewerControl
    {
        private readonly string drawingFilePath;
        private readonly string markupFilePath;

        public EModelViewControl ModelViewControl { get; private set; }

        public EModelMarkupControl ModelMarkupControl { get; private set; }

        public EDrawingsHost HostControl { get; private set; }

        public _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler OnFailedLoadingDocument { get; set; }

        public bool DeleteFilesOnDispose { get; set; } = false;

        public DrawingReviewerControl(string drawingFilePath)
        {
            this.drawingFilePath = drawingFilePath ?? throw new ArgumentNullException(nameof(drawingFilePath));
            HostControl = new EDrawingsHost();
            HostControl.ControlLoaded += OnControlLoaded;
        }

        public DrawingReviewerControl(string drawingFilePath, string markupFilePath)
        {
            this.drawingFilePath = drawingFilePath ?? throw new ArgumentNullException(nameof(drawingFilePath));
            this.markupFilePath = markupFilePath ?? throw new ArgumentNullException(nameof(markupFilePath));
            HostControl = new EDrawingsHost();
            HostControl.ControlLoaded += OnControlLoaded;
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            Log.Debug("eDrawings control has been loaded...");
            ModelViewControl = ctrl;
            ModelMarkupControl = (EModelMarkupControl)ModelViewControl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl");

            if (OnFailedLoadingDocument != null)
            {
                ModelViewControl.OnFailedLoadingDocument += OnFailedLoadingDocument;
            }
            Log.Debug($"Opening drawing {drawingFilePath}..");
            ModelViewControl.OpenDoc(drawingFilePath, false, false, false, "");
            HostControl.Dock = DockStyle.Fill;

            if (markupFilePath != null && File.Exists(markupFilePath))
            {
                Log.Debug($"Opening drawing markup file {markupFilePath}..");
                OpenMarkupFile(markupFilePath);
            }
        }

        public void SetTextWithLeaderMarkupOperator()
        {
            ModelMarkupControl.ViewOperator = EMVMarkupOperators.eMVOperatorMarkupTextWithLeader;
        }

        public void SetCloudWithLeaderMarkupOperator()
        {
            Log.Debug("Setting MarkupCloudWithLeader operator...");
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
                Log.Debug("Disposing EDrawingsHost...");
                HostControl.ControlLoaded -= OnControlLoaded;
                HostControl.Dispose(); 

                if (DeleteFilesOnDispose)
                {
                    Log.Debug("DrawingReviewerControl has been disposed, deleting files (if any)...");
                    if (drawingFilePath != null && File.Exists(drawingFilePath))
                        File.Delete(drawingFilePath);

                    if (markupFilePath != null && File.Exists(markupFilePath))
                        File.Delete(markupFilePath);
                }
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
