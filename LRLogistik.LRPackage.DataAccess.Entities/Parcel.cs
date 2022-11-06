using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class Parcel
    {
        public int ParcelId { get; set; }
        //Parcel 
        public float Weight { get; set; }

        public Recipient Recipient { get; set; }
        
        
        public Recipient Sender { get; set; }

        //Tracking information

        public enum StateEnum
        {
            PickupEnum = 1,
            InTransportEnum = 2,
            InTruckDeliveryEnum = 3,
            TransferredEnum = 4,
            DeliveredEnum = 5
        }

        public StateEnum State { get; set; }

        public List<HopArrival> VisitedHops { get; set; }

        public List<HopArrival> FutureHops { get; set; }

        //NewParcelInfo
        public string TrackingId { get; set; }
    }
}
