using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class WarehouseNextHops
    {
        public string WarehouseNextHopsId { get; set; }

        public int TraveltimeMins { get; set; }

        public Hop Hop { get; set; }
    }
}
