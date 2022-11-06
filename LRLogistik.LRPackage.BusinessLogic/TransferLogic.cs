﻿using AutoMapper;
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
    public class TransferLogic : ITransferLogic
    {

        //public TransferLogic() { }

        private readonly IMapper _mapper;
        IParcelRepository _parcelRepository;


        [ActivatorUtilitiesConstructor]
        public TransferLogic(IMapper mapper)
        {
            _mapper = mapper;
            _parcelRepository = new ParcelRepository(); 
        }

        public TransferLogic(IMapper mapper, IParcelRepository repository)
        {
            _mapper = mapper;
            _parcelRepository = repository; 
        }

        public object TransferPackage(string trackingId, Parcel parcel)
        {
            TrackingIdValidator trackingIdValidator = new TrackingIdValidator();
            ParcelValidator parcelValidator = new ParcelValidator();
            RecipientValidator recipientValidator = new RecipientValidator(); 

            var result_t = trackingIdValidator.Validate(trackingId);
            var result_c = parcelValidator.Validate(parcel);
            //var result_r = recipientValidator.Validate(parcel.Recipient);


            if (result_t.IsValid && result_c.IsValid)
            {
                parcel.TrackingId = trackingId;
                _parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));


                return new Parcel() { TrackingId = "111111111" };
            }
            else
            {
                return new Error() { ErrorMessage = "string" };
            }

        }
    }
}
