using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Exceptions;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.DataAccess.Sql;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
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
    public class TrackingLogic : ITrackingLogic
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        IParcelRepository _parcelRepository;
        IWarehouseRepository _warehouseRepository;
        IWebhookManager _webhookManager;

        public TrackingLogic(IMapper mapper, IParcelRepository repository, ILogger<TrackingLogic> logger, IWarehouseRepository warehouseRepository, IWebhookManager webhookManager)
        {
            _mapper = mapper;
            _parcelRepository = repository;
            _logger = logger;
            _warehouseRepository = warehouseRepository;
            _webhookManager = webhookManager;
        }

        public string ReportDelivery(string trackingId)
        {
            try
            {
                TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
                var result = trackingIdValidator.Validate(trackingId);
                if (!result.IsValid)
                {
                    throw new BusinessLogicValidationException("ValidateTrackingId", "Tracking Id Validation was invalid");
                }
                _logger.LogInformation($"Reporting delivery of TrackingId: {JsonConvert.SerializeObject(trackingId)}");

                _parcelRepository.UpdateDeliveryState(trackingId); 
                
                //TODO DELETE WEBHOOKS

                return "Successfully reported delivery";
            }
            catch(BusinessLogicValidationException e)
            {
                _logger.LogError($"TrackingId was invalid!");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("ReportDelivery", "Delivery was not reported", e);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"Unable to change data in database!");
                throw new BusinessLogicNotFoundException("ReportDelivery", "Delivery was not reported", e);
            }
        }

        public string ReportHop(string trackingId, string code)
        {
            try
            {
                TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
                TrackingCodeValidator trackingCodeValidator = new TrackingCodeValidator();

                var result_t = trackingIdValidator.Validate(trackingId);
                var result_c = trackingCodeValidator.Validate(code);
            
                if(!result_t.IsValid) 
                {
                    throw new BusinessLogicValidationException("ValidateTrackingId", "Tracking ID was invalid");
                }
                else if(!result_c.IsValid) 
                {
                    throw new BusinessLogicValidationException("ValidateTrackingCode", "Tracking Code was invalid");
                }

                //Aufruf an die österreichische Bevölkerung:
                //GEHEN SIE WÄHLEN!
                _parcelRepository.ReportHop(trackingId, code);

                _webhookManager.NotifySubscribers(trackingId);

                _logger.LogInformation($"Reporting Hop of TrackingId: {JsonConvert.SerializeObject(trackingId)} and code: {JsonConvert.SerializeObject(code)}");
                return "Successfully reported hop";
            }
            catch(BusinessLogicValidationException e) 
            {
                _logger.LogError($"TrackingId or Code was invalid!");
                throw new BusinessLogicNotFoundException("ReportHop", "Hop was not reported", e);
            }
            catch(DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"Unable to change data in database!");
                throw new BusinessLogicNotFoundException("ReportHop", "Hop was not reported", e);
            }
        }

        public Parcel TrackPackage(string trackingId)
        {
            try
            {
                TrackingIdValidator trackingIdValidator = new TrackingIdValidator();

                var result = trackingIdValidator.Validate(trackingId);

                if(!result.IsValid) 
                {
                    throw new BusinessLogicValidationException("ValidateTrackingId", "Tracking Id was invalid");
                }

                _logger.LogInformation($"Tracking Package with TrackingId: {JsonConvert.SerializeObject(trackingId)}");
                var parcel = _mapper.Map<BusinessLogic.Entities.Parcel>(_parcelRepository.GetByTrackingId(trackingId));
                _logger.LogInformation($"Found Parcel to track: {JsonConvert.SerializeObject(parcel)}");

                return parcel;
            }
            catch(BusinessLogicValidationException e)
            {
                _logger.LogError($"TrackingId was invalid!");
                throw new BusinessLogicNotFoundException("TrackPackage", "Package wasn't tracked", e);
            }
            catch(DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"Parcel was not found!");
                throw new BusinessLogicNotFoundException("TrackPackage", "Package wasn't tracked", e);
            }
        }

    }
}
