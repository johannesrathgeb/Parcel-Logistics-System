using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
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


        [ActivatorUtilitiesConstructor]
        public SubmissionLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SubmissionLogic()
        {

        }


        public object SubmitParcel(Parcel parcel)
        {
            ParcelValidator recipientValidator = new ParcelValidator();

            var result = recipientValidator.Validate(parcel); 

            if(result.IsValid)
            {
                parcel.TrackingId = RandomString(9);
                DataAccess.Sql.ParcelRepository parcelRepository = new DataAccess.Sql.ParcelRepository();
                parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));
                

                return new Parcel() { TrackingId = "333333333" };

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
