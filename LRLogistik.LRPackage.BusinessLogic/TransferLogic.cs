using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Exceptions;
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

        public Parcel TransferPackage(string trackingId, Parcel parcel)
        {
            try
            {
                TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
                ParcelValidator recipientValidator = new ParcelValidator();

                var result_t = trackingIdValidator.Validate(trackingId);
                var result_c = recipientValidator.Validate(parcel);

                if (!result_t.IsValid)
                {
                    throw new BusinessLogicValidationException("ValidateTrackingId", "Tracking ID was invalid");
                }
                else if (!result_c.IsValid)
                {
                    throw new BusinessLogicValidationException("ValidateRecipient", "Recipient was invalid");
                }

                _logger.LogInformation($"Transferring Package: {JsonConvert.SerializeObject(parcel)}");
                parcel.TrackingId = trackingId;
                _logger.LogInformation($"Package updated with TrackingId: {JsonConvert.SerializeObject(parcel)}");
                _logger.LogInformation($"Mapping Package to DataAccessLayer: {JsonConvert.SerializeObject(parcel)}");
                _parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));

                return parcel;
            }
            catch (BusinessLogicValidationException e)
            {
                _logger.LogError($"TrackingId or Recipient was invalid!");
                throw new BusinessLogicNotFoundException("TransferPackage", "Package was not transferred", e);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotCreatedException e)
            {
                _logger.LogError($"Package was not created!");
                throw new BusinessLogicNotFoundException("TransferPackage", "Package was not transferred", e);
            }
        }
    }
}
