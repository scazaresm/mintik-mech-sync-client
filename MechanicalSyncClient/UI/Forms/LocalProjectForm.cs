using MechanicalSyncClient.Core;
using MechanicalSyncClient.Core.ApiClient;
using MechanicalSyncClient.Core.Domain;
using MechanicalSyncClient.Database;
using MechanicalSyncClient.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncClient
{
    public partial class LocalProjectForm : Form
    {
        private ProjectMonitor monitor;

        public LocalProjectForm()
        {
            InitializeComponent();
        }

        private async Task RunDemo()
        {
            int remoteIdToCheck = 123; 
            LocalProject project = await DB.Connection.Table<LocalProject>()
                                              .Where(p => p.RemoteId == remoteIdToCheck)
                                              .FirstOrDefaultAsync();

            if (project == null)
            {
                project = new LocalProject
                {
                    RemoteId = remoteIdToCheck,
                    FullPath = "C:\\sync_demo",
                    DownloadDateTime = DateTime.Now,
                    LastSyncDateTime = DateTime.Now
                };

                await DB.Connection.InsertAsync(project);
                Console.WriteLine("Demo project inserted.");
            }
            else
            {
                Console.WriteLine("Demo project already exists.");
            }

            monitor = new ProjectMonitor(project, "*.sldprt | *.slddrw | *.sldasm");
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
            string localFilename = Guid.NewGuid().ToString() + ".SLDASM";
            _ = MechSyncClient.Instance.DownloadProjectFileAsync("working-folder/123/M097_00-00.SLDASM", localFilename);
        }

        public void UpdateToolStripProgressBar(int value)
        {
            if (toolStripProgressBar1.Owner.InvokeRequired)
            {
                toolStripProgressBar1.Owner.Invoke(new Action(() => UpdateToolStripProgressBar(value)));
            }
            else
            {
                toolStripProgressBar1.Value = value;
            }
        }
    }
}
