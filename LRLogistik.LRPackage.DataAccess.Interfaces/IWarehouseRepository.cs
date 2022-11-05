using LRLogistik.LRPackage.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Interfaces
{
    public interface IWarehouseRepository
    {
        void Create(Warehouse w);
        Warehouse Update(Warehouse w);
        void Delete(int id);

        // Example with multiple GETs
        IEnumerable<Warehouse> GetByXX(string xx);
        Warehouse GetByYY(int yy);

        // Get by ID
        Warehouse GetByTrackingId(string trackingid);
    }
}
