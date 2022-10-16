using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Validators
{
    public class TrackingIdValidator : AbstractValidator<string>
    {
        public TrackingIdValidator()
        {
            RuleFor(trackingId => trackingId).NotNull().DependentRules(() => {
                RuleFor(trackingId => trackingId).Must(BeAValidTrackingId);
            });
        }

        public bool BeAValidTrackingId(string trackingId)
        {
            Regex regex = new Regex(@"^[A-Z0-9]{9}$");

            return regex.IsMatch(trackingId);
        }
    }
}
