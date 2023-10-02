using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp
{
    public partial class LocalProjectForm : Form
    {
        private ProjectChangeMonitor monitor;

        public LocalProjectForm()
        {
            InitializeComponent();
        }

        private async Task RunDemo()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (monitor == null)
            {
                try
                {
                    _ = RunDemo();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift)
                MessageBox.Show("Hey you pressed Shift!");
        }

        private void tabControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                MessageBox.Show("You want to save your changes!");
                e.Handled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

     
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
