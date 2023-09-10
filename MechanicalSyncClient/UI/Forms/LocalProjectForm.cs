using MechanicalSyncClient.Core;
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
    public partial class MainForm : Form
    {
        private LocalProjectMonitor monitor;

        public MainForm()
        {
            InitializeComponent();
        }

        private async Task RunDemo()
        {
            int remoteIdToCheck = 123; 
            LocalProject project = await DB.Async.Table<LocalProject>()
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

                await DB.Async.InsertAsync(project);
                Console.WriteLine("Demo project inserted.");
            }
            else
            {
                Console.WriteLine("Demo project already exists.");
            }

            monitor = new LocalProjectMonitor(project, "*.sldprt | *.slddrw | *.sldasm");
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
    }
}
