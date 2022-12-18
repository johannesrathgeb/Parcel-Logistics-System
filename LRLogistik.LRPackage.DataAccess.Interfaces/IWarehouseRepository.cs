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
        Warehouse Create(Warehouse w);
        Warehouse Update(Warehouse w);
        void Delete(string id);
        Warehouse ExportHierachy(); 

        // Get by ID
        Warehouse GetByHopId(string id);
        Hop GetByHopCode(string code); 
    }
}
