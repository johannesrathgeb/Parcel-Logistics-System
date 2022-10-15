using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class TransferLogic : ITransferLogic
    {

        public TransferLogic() { }

        public object TransferPackage(string trackingId, Parcel parcel)
        {
            return new Parcel() { TrackingId = "gaugla"};
        }
    }
}
