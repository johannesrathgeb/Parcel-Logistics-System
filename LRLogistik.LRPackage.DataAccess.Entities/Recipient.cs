﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class Recipient
    {
        public string RecipientId { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }


        public string PostalCode { get; set; }


        public string City { get; set; }


        public string Country { get; set; }

    }

}
