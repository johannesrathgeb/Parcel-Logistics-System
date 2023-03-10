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
using LRLogistik.LRPackage.ServiceAgents;
using LRLogistik.LRPackage.BusinessLogic.Exceptions;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
using NetTopologySuite.Geometries;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class SubmissionLogic : ISubmissionLogic
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IGeoEncodingAgent _encodingagent; 

        private static Random random = new Random();
        IParcelRepository _parcelRepository;

        public SubmissionLogic(IMapper mapper, IParcelRepository repository, ILogger<SubmissionLogic> logger, IGeoEncodingAgent encodingAgent)
        {
            _mapper = mapper;
            _parcelRepository = repository;
            _logger = logger;
            _encodingagent = encodingAgent; 
        }

        public Parcel SubmitParcel(Parcel parcel)
        {

            try
            {
                ParcelValidator recipientValidator = new ParcelValidator();
                var result = recipientValidator.Validate(parcel);
                if(result.IsValid == false)
                {
                    throw new BusinessLogicValidationException("ValidateParcel", "Parcel was invalid");
                }
                parcel.TrackingId = RandomString(9);
                _logger.LogInformation($"New TrackingId created: {JsonConvert.SerializeObject(parcel.TrackingId)}");

                var senderCoordinates = _mapper.Map<Point>(_encodingagent.EncodeAddress(parcel.Sender));
                var recipientCoordinates = _mapper.Map<Point>(_encodingagent.EncodeAddress(parcel.Recipient));


                _logger.LogInformation($"Sender and Recipient coordinates retrieved");

                parcel.State = Parcel.StateEnum.PickupEnum; 
                _logger.LogInformation($"Set Package to Pickup-State: {JsonConvert.SerializeObject(parcel)}");

                DataAccess.Entities.Parcel p = (DataAccess.Entities.Parcel)_parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel), senderCoordinates, recipientCoordinates);
                _logger.LogInformation($"Parcel submitted to DAL");
                return _mapper.Map<BusinessLogic.Entities.Parcel>(p);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotCreatedException e)
            {
                _logger.LogError($"Parcel with ID {parcel.TrackingId} not created in DB");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("SubmitParcel", "Parcel was not submitted", e);
            }
            catch (BusinessLogicValidationException e) 
            {
                _logger.LogError($"Parcel with ID {parcel.TrackingId} not created in DB");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("SubmitParcel", "Parcel was not submitted", e);
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
