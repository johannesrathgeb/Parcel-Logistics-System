using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class Parcel
    {
        //Parcel 


        public float Weight { get; set; }

        public Recipient Recipient { get; set; }

        public Recipient Sender { get; set; }


        //Tracking information

        public enum StateEnum
        {

            PickupEnum = 1,

            InTransportEnum = 2,

            InTruckDeliveryEnum = 3,

            TransferredEnum = 4,

            DeliveredEnum = 5
        }

        public StateEnum State { get; set; }


        public List<HopArrival> VisitedHops { get; set; }

        public List<HopArrival> FutureHops { get; set; }

        //NewParcelInfo

        public string TrackingId { get; set; }



    }

    public class ParcelValidator : AbstractValidator<Parcel>
    {
        public ParcelValidator()
        {
            RuleFor(parcel => parcel.VisitedHops).NotNull();
            RuleFor(parcel => parcel.FutureHops).NotNull();
            RuleFor(parcel => parcel.Recipient).NotNull();
            RuleFor(parcel => parcel.Sender).NotNull();
            RuleFor(parcel => parcel.State).NotNull();
            RuleFor(parcel => parcel.Weight > 0.0);
            RuleFor(parcel => parcel.TrackingId).Must(BeAValidTrackingId);
        }

        public bool BeAValidTrackingId(string city)
        {
            Regex regex = new Regex(@"^[A-Z0-9]{9}$");

            return regex.IsMatch(city);
        }
    }

}
