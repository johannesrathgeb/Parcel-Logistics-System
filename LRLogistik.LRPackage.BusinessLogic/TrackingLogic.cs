using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class TrackingLogic : ITrackingLogic
    {
        private readonly IMapper _mapper;
        IParcelRepository _parcelRepository;  

        [ActivatorUtilitiesConstructor]
        public TrackingLogic(IMapper mapper) {
            _mapper = mapper;
            _parcelRepository = new ParcelRepository(); 
        }

        public TrackingLogic()
        {
            
        }

        public object ReportDelivery(string trackingId)
        {
            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();

            var result = trackingIdValidator.Validate(trackingId);

            if (result.IsValid)
            {
                return "Successfully reported hop";
            }
            else
            {
                return new Error() { ErrorMessage = "string" };
            }
        }

        public object ReportHop(string trackingId, string code)
        {

            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
            TrackingCodeValidator trackingCodeValidator = new TrackingCodeValidator();

            var result_t = trackingIdValidator.Validate(trackingId);
            var result_c = trackingCodeValidator.Validate(code);

            if (result_t.IsValid && result_c.IsValid)
            {
                return "Successfully reported hop";
            }
            else
            {
                return new Error() { ErrorMessage = "string" };
            }
        }

        public object TrackPackage(string trackingId)
        {

            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();

            var result = trackingIdValidator.Validate(trackingId);

            if (result.IsValid)
            {

                return _mapper.Map<BusinessLogic.Entities.Parcel>(_parcelRepository.GetByTrackingId(trackingId));


                /*
                return new Parcel()
                {
                    TrackingId = "111111111",
                    Weight = 0.0f,
                    Recipient = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                    Sender = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                    State = Parcel.StateEnum.InTruckDeliveryEnum,
                    VisitedHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
                    FutureHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
                };
                */
            }
            else
            {
                return new Error() { ErrorMessage = "string" };
            }

        }

    }
}
