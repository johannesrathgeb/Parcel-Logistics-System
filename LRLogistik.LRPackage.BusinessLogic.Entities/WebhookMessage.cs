﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public  class WebhookMessage
    {
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

        public string TrackingId { get; set; }

    }
}
