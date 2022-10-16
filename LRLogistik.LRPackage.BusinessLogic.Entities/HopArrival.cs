using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class HopArrival
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

    }

}
