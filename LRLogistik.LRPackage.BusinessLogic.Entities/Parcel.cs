using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    
    public class Parcel
    {
        //Parcel 

        public float Weight { get; set; }

        [NotMapped]
        public Recipient Recipient { get; set; }
        [NotMapped]
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
        [NotMapped]
        public List<HopArrival> VisitedHops { get; set; }
        [NotMapped]
        public List<HopArrival> FutureHops { get; set; }

        //NewParcelInfo
        [Key]
        public string TrackingId { get; set; }
    }

}
