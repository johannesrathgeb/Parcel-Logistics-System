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
    public class ParcelValidator : AbstractValidator<Parcel>
    {
        public ParcelValidator()
        {
            RuleFor(parcel => parcel.Recipient).NotNull();
            RuleFor(parcel => parcel.Sender).NotNull();
            RuleFor(parcel => parcel.Weight).GreaterThan(0.0f);
        }
        /*
        public bool BeAValidTrackingId(string trackingId)
        {
            Regex regex = new Regex(@"^[A-Z0-9]{9}$");

            return regex.IsMatch(trackingId);
        }
        */
    }
}
