using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    public class Warehouse : Hop
    {

        public int Level { get; set; }

        public List<WarehouseNextHops> NextHops { get; set; }

    }
}
