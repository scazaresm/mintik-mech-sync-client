﻿using eDrawings.Interop.EModelMarkupControl;
using eDrawings.Interop.EModelViewControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class DesignFileViewerControl : IDisposable
    {
        private readonly string filePath;

        public EModelViewControl ModelViewControl { get; private set; }

        public EModelMarkupControl ModelMarkupControl { get; private set; }

        public EDrawingsHost HostControl { get; private set; }

        public _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler OnFailedLoadingDocument { get; set; }

        public DesignFileViewerControl(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            HostControl = new EDrawingsHost();
            HostControl.Dock = DockStyle.Fill;
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
        }

        public void SetMeasureOperator()
        {
            ModelMarkupControl.ViewOperator = EMVMarkupOperators.eMVOperatorMeasure;
        }

        public void SetPanOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorPan;
        }

        public void SetRotateOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorRotate;
        }

        public void SetSelectOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorSelect;
        }

        public void SetZoomOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorZoom;
        }

        public void SetZoomToAreaOperator()
        {
            ModelViewControl.ViewOperator = EMVOperators.eMVOperatorZoomToArea;
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
