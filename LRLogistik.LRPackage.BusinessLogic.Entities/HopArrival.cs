﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class HopArrival
    {
        [Key]
        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

    }

}
