using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.Services.DTOs;

namespace LRLogistik.LRPackage.Services.MappingProfiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<DTOs.Parcel, BusinessLogic.Entities.Parcel>()
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src.Recipient))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender));

            CreateMap<BusinessLogic.Entities.Parcel, DTOs.NewParcelInfo>()
                .ForMember(dest => dest.TrackingId, opt => opt.MapFrom(src => src.TrackingId));
        }
    }
}
