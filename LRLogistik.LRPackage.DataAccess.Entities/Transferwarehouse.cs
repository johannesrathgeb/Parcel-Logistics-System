using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class Transferwarehouse : Hop
    {

        public Geometry Region { get; set; }

        public string LogisticsPartner { get; set; }

        public string LogisticsPartnerUrl { get; set; }
    }
}
