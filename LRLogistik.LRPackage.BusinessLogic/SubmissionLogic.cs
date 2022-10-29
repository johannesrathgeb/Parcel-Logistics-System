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
                return new Parcel() { TrackingId = "333333333" };
            } else
            {
                return new Error() { ErrorMessage = "string" };
            }

        }
    }
}
