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
    public class SubmissionLogic : ISubmissionLogic
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private static Random random = new Random();
        IParcelRepository _parcelRepository;

        public SubmissionLogic(IMapper mapper, IParcelRepository repository, ILogger<SubmissionLogic> logger)
        {
            _mapper = mapper;
            _parcelRepository = repository;
            _logger = logger;
        }

        public object SubmitParcel(Parcel parcel)
        {
<<<<<<< HEAD
            ParcelValidator parcelValidator = new ParcelValidator();
            RecipientValidator recipientValidator = new RecipientValidator(); 
=======
            _logger.LogInformation($"Submitting parcel {JsonConvert.SerializeObject(parcel)}");
            ParcelValidator recipientValidator = new ParcelValidator();
>>>>>>> dev1

            var result = parcelValidator.Validate(parcel);
            //var result_r = recipientValidator.Validate(parcel.Recipient);

            if (result.IsValid)
            {
                parcel.TrackingId = RandomString(9);
                _logger.LogInformation($"New TrackingId created: {JsonConvert.SerializeObject(parcel.TrackingId)}");
                DataAccess.Entities.Parcel p = (DataAccess.Entities.Parcel)_parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));
                _logger.LogInformation($"Parcel submitted to DAL");
                return _mapper.Map<BusinessLogic.Entities.Parcel>(p);
            } else
            {
                _logger.LogDebug($"Recipient was invalid!");
                return new Error() { ErrorMessage = "string" };
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
