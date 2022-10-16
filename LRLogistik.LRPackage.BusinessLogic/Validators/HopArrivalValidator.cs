using FluentValidation;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Validators
{
    public class HopArrivalValidator : AbstractValidator<HopArrival>
    {
        public HopArrivalValidator()
        {
            RuleFor(hopArrival => hopArrival).NotNull();

            RuleFor(hopArrival => hopArrival.Code).NotNull().DependentRules(() => {
                RuleFor(hopArrival => hopArrival.Code).Must(BeAValidCode);
            });

            RuleFor(hopArrival => hopArrival.Description).NotNull().DependentRules(() => {
                RuleFor(hopArrival => hopArrival.Description).Must(BeAValidDescription);
            });

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
