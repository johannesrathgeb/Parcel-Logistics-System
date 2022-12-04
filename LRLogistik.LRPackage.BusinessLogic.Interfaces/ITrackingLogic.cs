using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
        public Entities.Parcel TrackPackage(string trackingId);

        public string ReportDelivery(string trackingId);

        public string ReportHop(string trackingId, string code); 

    }
}
