using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    public class HopArrival
    {
        public int HopArrivalId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

    }
}
