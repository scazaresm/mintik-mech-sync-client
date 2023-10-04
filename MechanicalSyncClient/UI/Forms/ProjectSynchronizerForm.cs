using MechanicalSyncApp.Sync.ProjectSynchronizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ProjectSynchronizerForm : Form
    {
        private readonly ProjectSynchronizer synchronizer;

        public ProjectSynchronizerForm(ProjectSynchronizer synchronizer)
        {
            InitializeComponent();
            this.synchronizer = synchronizer;
        }


    }
}
