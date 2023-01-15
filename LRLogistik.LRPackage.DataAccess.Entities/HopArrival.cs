using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class HopArrival
    {
        public string HopArrivalId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public int HopOrder { get; set; }

    }
}
