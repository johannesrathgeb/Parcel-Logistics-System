using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class TransferLogic : ITransferLogic
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        IParcelRepository _parcelRepository;

        public TransferLogic(IMapper mapper, IParcelRepository repository, ILogger<TransferLogic> logger)
        {
            _mapper = mapper;
            _parcelRepository = repository; 
            _logger = logger;
        }

        public object TransferPackage(string trackingId, Parcel parcel)
        {
            _logger.LogInformation($"Transferring Package: {JsonConvert.SerializeObject(parcel)}");
            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
            ParcelValidator parcelValidator = new ParcelValidator();
            RecipientValidator recipientValidator = new RecipientValidator(); 

            var result_t = trackingIdValidator.Validate(trackingId);
            var result_c = parcelValidator.Validate(parcel);
            //var result_r = recipientValidator.Validate(parcel.Recipient);


            if (result_t.IsValid && result_c.IsValid)
            {
                parcel.TrackingId = trackingId;
                _logger.LogInformation($"Package updated with TrackingId: {JsonConvert.SerializeObject(parcel)}");
                _logger.LogInformation($"Mapping Package to DataAccessLayer: {JsonConvert.SerializeObject(parcel)}");
                _parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));


                return new Parcel() { TrackingId = "111111111" };
            }
            else
            {
                _logger.LogDebug($"TrackingId or Recipient was invalid!");
                return new Error() { ErrorMessage = "string" };
            }

        }
    }
}
