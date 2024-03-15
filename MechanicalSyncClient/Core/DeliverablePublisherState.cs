using MechanicalSyncApp.Publishing.DeliverablePublisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public abstract class DeliverablePublisherState
    {
        public IDeliverablePublisher Publisher { get; private set; }

        public void SetPublisher(IDeliverablePublisher publisher)
        {
            Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }
        public abstract void UpdateUI();
        public abstract Task RunAsync();
    }
}
