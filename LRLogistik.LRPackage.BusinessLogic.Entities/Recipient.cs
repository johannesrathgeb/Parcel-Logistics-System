using FluentValidation;
using System.Text.RegularExpressions; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class Recipient
    {

        public string Name { get; set; }


        public string Street { get; set; }


        public string PostalCode { get; set; }


        public string City { get; set; }


        public string Country { get; set; }
    }



}
