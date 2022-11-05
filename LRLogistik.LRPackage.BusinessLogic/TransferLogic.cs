using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class TransferLogic : ITransferLogic
    {

        //public TransferLogic() { }

        private readonly IMapper _mapper;

        [ActivatorUtilitiesConstructor]
        public TransferLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TransferLogic()
        {
            
        }

        public object TransferPackage(string trackingId, Parcel parcel)
        {
            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
            ParcelValidator recipientValidator = new ParcelValidator();

            var result_t = trackingIdValidator.Validate(trackingId);
            var result_c = recipientValidator.Validate(parcel);


            if (result_t.IsValid && result_c.IsValid)
            {
                DataAccess.Sql.ParcelRepository parcelRepository = new DataAccess.Sql.ParcelRepository();
                parcel.TrackingId = trackingId; 
                parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));


                return new Parcel() { TrackingId = "111111111" };
            }
            else
            {
                return new Error() { ErrorMessage = "string" };
            }

        }
    }
}
