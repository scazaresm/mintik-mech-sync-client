using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenDrawingForViewingCommand : IVersionSynchronizerCommandAsync
    {
        private readonly Review review;
        private readonly ReviewTarget drawingReviewTarget;

        public IVersionSynchronizer Synchronizer { get; private set; }

        public OpenDrawingForViewingCommand(IVersionSynchronizer synchronizer, Review review, ReviewTarget drawingReviewTarget)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.review = review ?? throw new ArgumentNullException(nameof(review));
            this.drawingReviewTarget = drawingReviewTarget ?? throw new ArgumentNullException(nameof(drawingReviewTarget));
        }

        public async Task RunAsync()
        {
            try
            {
                Console.WriteLine($"Opening target {drawingReviewTarget.Id} from review {review.Id}");
            }
            catch(Exception ex)
            {
                var message = $"Could not open drawing for viewing, {ex}";
                Log.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
