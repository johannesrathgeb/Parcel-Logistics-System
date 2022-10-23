using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    public class WarehouseNextHops
    {
        public int TraveltimeMins { get; set; }

        public Hop Hop { get; set; }
    }
}
