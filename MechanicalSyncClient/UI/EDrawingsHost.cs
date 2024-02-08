using eDrawings.Interop.EModelViewControl;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
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
        private static string GetCLSID()
        {
            var clsid = ConfigurationManager.AppSettings["EDRAWINGS_VIEWER_CLSID"];

            return clsid ?? throw new Exception("Missing EDRAWINGS_VIEWER_CLSID configuration entry in config file.");
        }
    }
}
