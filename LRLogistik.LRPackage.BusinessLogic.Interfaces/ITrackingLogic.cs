using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
        public object TrackPackage(string trackingId);

        public object ReportDelivery(string trackingId);

        public object ReportHop(string trackingId, string code); 

    }
}
