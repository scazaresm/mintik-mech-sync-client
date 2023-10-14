using eDrawings.Interop.EModelMarkupControl;
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
    public class DesignViewerControl
    {
        private readonly string filePath;

        public class EDrawingsHost : AxHost
        {
            public event Action<EModelViewControl> ControlLoaded;

            private bool m_IsLoaded;


            public EDrawingsHost() : base(GetCLSID())
            {
                m_IsLoaded = false;
            }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();

                if (!m_IsLoaded)
                {
                    m_IsLoaded = true;
                    var ctrl = this.GetOcx() as EModelViewControl;
                    ControlLoaded?.Invoke(this.GetOcx() as EModelViewControl);
                }
            }
        }

        private EModelViewControl modelViewControl;
        private EModelMarkupControl modelMarkupControl;
        private _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocument;

        public EDrawingsHost HostControl { get; private set; }

        public DesignViewerControl(string filePath, _IEModelViewControlEvents_OnFailedLoadingDocumentEventHandler onFailedLoadingDocument)
        {
            this.filePath = filePath;

            HostControl = new EDrawingsHost();
            HostControl.ControlLoaded += OnControlLoaded;
            HostControl.Dock = DockStyle.Fill;
            this.onFailedLoadingDocument = onFailedLoadingDocument ?? throw new ArgumentNullException(nameof(onFailedLoadingDocument));
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            modelViewControl = ctrl;
            modelViewControl.OnFailedLoadingDocument += onFailedLoadingDocument;
            modelMarkupControl = modelViewControl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl");

            modelViewControl.OpenDoc(filePath, false, false, false, "");
        }

        public void SetMeasureOperator()
        {
            modelMarkupControl.ViewOperator = EMVMarkupOperators.eMVOperatorMeasure;
        }

        public void SetPanOperator()
        {
            modelViewControl.ViewOperator = EMVOperators.eMVOperatorPan;
        }

        public void SetRotateOperator()
        {
            modelViewControl.ViewOperator = EMVOperators.eMVOperatorRotate;
        }

        public void SetSelectOperator()
        {
            modelViewControl.ViewOperator = EMVOperators.eMVOperatorSelect;
        }

        public void SetZoomOperator()
        {
            modelViewControl.ViewOperator = EMVOperators.eMVOperatorZoom;
        }

        public void SetZoomToAreaOperator()
        {
            modelViewControl.ViewOperator = EMVOperators.eMVOperatorZoomToArea;
        }

        private static string GetCLSID()
        {
            return "476F8C77-404C-4876-BCCA-0215E6B8BB14";
        }
    }
}
