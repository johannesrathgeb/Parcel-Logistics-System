using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    public class Truck : Hop
    {
        public Geometry Region { get; set; }

        public string NumberPlate { get; set; }
    }
}
