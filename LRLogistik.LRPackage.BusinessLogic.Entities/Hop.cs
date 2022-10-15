using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class Hop
    {

        public string HopType { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int ProcessingDelayMins { get; set; }

        public string LocationName { get; set; }

        public GeoCoordinate LocationCoordinates { get; set; }
    }

}

