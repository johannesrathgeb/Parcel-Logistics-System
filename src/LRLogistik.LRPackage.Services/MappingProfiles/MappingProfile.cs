using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Converters;

namespace LRLogistik.LRPackage.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Parcel, BusinessLogic.Entities.Parcel>()
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src.Recipient))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender))
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Parcel, DTOs.NewParcelInfo>()
                .ForMember(dest => dest.TrackingId, opt => opt.MapFrom(src => src.TrackingId))
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Parcel, DTOs.TrackingInformation>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.VisitedHops, opt => opt.MapFrom(src => src.VisitedHops))
                .ForMember(dest => dest.FutureHops, opt => opt.MapFrom(src => src.FutureHops))
                .ReverseMap();


            CreateMap<BusinessLogic.Entities.Error, DTOs.Error>().ReverseMap();

            CreateMap<DTOs.GeoCoordinate, BusinessLogic.Entities.GeoCoordinate>().ReverseMap();

            CreateMap<DTOs.Hop, BusinessLogic.Entities.Hop>().ReverseMap();

            CreateMap<DTOs.HopArrival, BusinessLogic.Entities.HopArrival>().ReverseMap();

            CreateMap<DTOs.Recipient, BusinessLogic.Entities.Recipient>().ReverseMap();

            CreateMap<DTOs.Transferwarehouse, BusinessLogic.Entities.Transferwarehouse>().ReverseMap();
            CreateMap<DTOs.Truck, BusinessLogic.Entities.Truck>()
                .IncludeBase<DTOs.Hop, BusinessLogic.Entities.Hop>()
                .ReverseMap();

            CreateMap<DTOs.Warehouse, BusinessLogic.Entities.Warehouse>()
                .IncludeBase<DTOs.Hop, BusinessLogic.Entities.Hop>()
                .ReverseMap();

            CreateMap<DTOs.WarehouseNextHops, BusinessLogic.Entities.WarehouseNextHops>().ReverseMap();


            //Test

            CreateMap<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .ForMember(dest => dest.LocationCoordinates, opt => opt.ConvertUsing(new GeoPointConverter(), src => src.LocationCoordinates))
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Transferwarehouse, DataAccess.Entities.Transferwarehouse>()
                .ForMember(dest => dest.Region, opt => opt.ConvertUsing(new GeoJsonConverter(), src => src.RegionGeoJson))
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Truck, DataAccess.Entities.Truck>()
                .ForMember(dest => dest.Region, opt => opt.ConvertUsing(new GeoJsonConverter(), src => src.RegionGeoJson))
                .IncludeBase<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Parcel, DataAccess.Entities.Parcel>().ReverseMap();
            CreateMap<BusinessLogic.Entities.Error, DataAccess.Entities.Error>().ReverseMap();
            CreateMap<BusinessLogic.Entities.HopArrival, DataAccess.Entities.HopArrival>().ReverseMap();
            CreateMap<BusinessLogic.Entities.Recipient, DataAccess.Entities.Recipient>().ReverseMap();
            CreateMap<BusinessLogic.Entities.Warehouse, DataAccess.Entities.Warehouse>()
                .IncludeBase<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .ReverseMap();
            CreateMap<BusinessLogic.Entities.WarehouseNextHops, DataAccess.Entities.WarehouseNextHops>().ReverseMap();
            CreateMap<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>().ReverseMap();
        }


    }
}
