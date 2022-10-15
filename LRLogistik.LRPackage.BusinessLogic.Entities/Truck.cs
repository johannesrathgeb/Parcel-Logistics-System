using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class Truck : Hop
    {
        public string RegionGeoJson { get; set; }

        public string NumberPlate { get; set; }
    }

}
