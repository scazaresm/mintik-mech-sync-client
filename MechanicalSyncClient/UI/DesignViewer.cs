using eDrawings.Interop.EModelViewControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class DesignViewer
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

        public EDrawingsHost HostControl { get; private set; }

        public DesignViewer(string filePath)
        {
            this.filePath = filePath;

            HostControl = new EDrawingsHost();
            HostControl.ControlLoaded += OnControlLoaded;
            HostControl.Dock = DockStyle.Fill;
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            ctrl.OpenDoc(filePath, false, false, false, "");
        }

        private static string GetCLSID()
        {
            return "476F8C77-404C-4876-BCCA-0215E6B8BB14";
        }
    }
}
