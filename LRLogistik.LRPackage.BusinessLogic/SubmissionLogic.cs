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
    public class SubmissionLogic : ISubmissionLogic
    {
        /*
        IParcelRepository _parcelRepository;

        public SubmissionLogic(IParcelRepository repo) {
            _parcelRepository = repo;
        }
        */
        private readonly IMapper _mapper;
        private static Random random = new Random();
        IParcelRepository _parcelRepository;


        [ActivatorUtilitiesConstructor]
        public SubmissionLogic(IMapper mapper)
        {
            _mapper = mapper;
            _parcelRepository = new ParcelRepository();
        }

        public SubmissionLogic(IMapper mapper, IParcelRepository repository)
        {
            _mapper = mapper;
            _parcelRepository = repository; 
        }

        public object SubmitParcel(Parcel parcel)
        {
            ParcelValidator recipientValidator = new ParcelValidator();

            var result = recipientValidator.Validate(parcel); 

            if(result.IsValid)
            {
                parcel.TrackingId = RandomString(9);
                DataAccess.Entities.Parcel p = _parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));

                return _mapper.Map<BusinessLogic.Entities.Parcel>(p);

            } else
            {
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
