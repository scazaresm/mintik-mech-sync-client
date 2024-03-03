using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using Serilog;
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
    public partial class AssemblyReviewerForm : Form
    {
        private readonly IAuthenticationServiceClient authServiceClient;
        private readonly IMechSyncServiceClient syncServiceClient;
        private readonly LocalReview review;
        private readonly ILogger logger;

        public AssemblyReviewerForm(IAuthenticationServiceClient authServiceClient,
                                   IMechSyncServiceClient syncServiceClient,
                                   LocalReview review,
                                   ILogger logger
            )
        {
            InitializeComponent();
            this.authServiceClient = authServiceClient ?? throw new ArgumentNullException(nameof(authServiceClient));
            this.syncServiceClient = syncServiceClient ?? throw new ArgumentNullException(nameof(syncServiceClient));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void AssemblyReviewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VersionSynchronizerForm.Instance.Show();
        }
    }
}
