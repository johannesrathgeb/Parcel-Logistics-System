using LRLogistik.LRPackage.DataAccess.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        Warehouse GetParent(Hop hop);
        List<HopArrival> Routing(Hop hopA, Hop hopB);
        Parcel Create(Parcel p, Point senderPoint, Point recipientPoint);
        void UpdateDeliveryState(string trackingid);
        void Delete(string TrackingId);

        // Get by ID
        Parcel GetByTrackingId(string trackingid);
        void ReportHop(string trackingId, string code);
    }
}
