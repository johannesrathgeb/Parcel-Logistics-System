using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class SubmissionLogic : ISubmissionLogic
    {
        /*
        IParcelRepository _parcelRepository;

        public SubmissionLogic(IParcelRepository repo) {
            _parcelRepository = repo;
        }
        */

        public object SubmitParcel(Parcel parcel)
        {
            ParcelValidator recipientValidator = new ParcelValidator();

            var result = recipientValidator.Validate(parcel); 

            if(result.IsValid)
            {
                DataAccess.Sql.ParcelRepository parcelRepository = new DataAccess.Sql.ParcelRepository();
                Parcel parcel123= new Parcel()
                {
                    TrackingId = "111111111",
                    Weight = 1.0f,
                    Recipient = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                    Sender = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                    State = Parcel.StateEnum.InTruckDeliveryEnum,
                    VisitedHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
                    FutureHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },

                };
                parcelRepository.Create(parcel123);

                return new Parcel() { TrackingId = "333333333" };

            } else
            {
                return new Error() { ErrorMessage = "string" };
            }

        }
    }
}
