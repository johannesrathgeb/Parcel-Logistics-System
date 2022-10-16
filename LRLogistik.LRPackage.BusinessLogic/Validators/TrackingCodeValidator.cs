using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Validators
{

    public class TrackingCodeValidator : AbstractValidator<string>
    {
        public TrackingCodeValidator()
        {
            RuleFor(trackingCode => trackingCode).NotNull().DependentRules(() => {
                RuleFor(trackingCode => trackingCode).Must(BeAValidCode);
            });
        }

        public bool BeAValidCode(string code)
        {
            Regex regex = new Regex(@"^[A-Z]{4}\d{1,4}$");

            return regex.IsMatch(code);
        }
    }
}
