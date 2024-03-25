using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class CloseAssemblyReviewCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IAssemblyReviewer Reviewer { get; }

        public CloseAssemblyReviewCommand(IAssemblyReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            var ui = Reviewer.Args.UI;
            try
            {
                var fileName = Path.GetFileName(Reviewer.AssemblyMetadata.RelativeFilePath);
                var sw = Reviewer.Args.SolidWorksStarter;
                (sw as SolidWorksStarter).SolidWorksApp.CloseDoc(fileName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);   
            }
            finally
            {
                ui.HideReviewPanel();
            }
        }
    }
}
