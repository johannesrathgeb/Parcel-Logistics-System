using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Interfaces
{
    public interface ITransferLogic
    {
        public void TransferPackage(string trackingId); 
    }
}
