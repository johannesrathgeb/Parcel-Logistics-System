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


    public class HopArrivalValidator : AbstractValidator<HopArrival>
    {
        public HopArrivalValidator()
        {
            RuleFor(hopArrival => hopArrival.Code).Must(BeAValidCode);
            RuleFor(hopArrival => hopArrival.Description).Must(BeAValidDescription);
            RuleFor(hopArrival => hopArrival).NotNull();
        }


        public bool BeAValidCode(string code)
        {
            Regex regex = new Regex(@"^[A-Z]{4}\d{1,4}$");

            return regex.IsMatch(code);
        }

        public bool BeAValidDescription(string description)
        {
            Regex regex = new Regex(@"^[A-Za-zäüöß /-]+$");

            return regex.IsMatch(description);
        }

    }
}
