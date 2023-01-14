using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Converters;
using NetTopologySuite.Geometries;
using System.Diagnostics.CodeAnalysis;


namespace LRLogistik.LRPackage.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        [ExcludeFromCodeCoverage]
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


            CreateMap<DTOs.GeoCoordinate, BusinessLogic.Entities.GeoCoordinate>().ReverseMap();

            CreateMap<DTOs.Hop, BusinessLogic.Entities.Hop>().ReverseMap();

            CreateMap<DTOs.HopArrival, BusinessLogic.Entities.HopArrival>().ReverseMap();

            CreateMap<DTOs.Recipient, BusinessLogic.Entities.Recipient>().ReverseMap();

            CreateMap<DTOs.Transferwarehouse, BusinessLogic.Entities.Transferwarehouse>().ReverseMap();

            CreateMap<DTOs.WebhookMessage, BusinessLogic.Entities.WebhookMessage>().ReverseMap();
            CreateMap<DataAccess.Entities.WebhookMessage, BusinessLogic.Entities.WebhookMessage>().ReverseMap();

            CreateMap<DataAccess.Entities.WebhookResponse, BusinessLogic.Entities.WebhookResponse>().ReverseMap();
            CreateMap<DTOs.WebhookResponse, BusinessLogic.Entities.WebhookResponse>().ReverseMap();

            CreateMap<BusinessLogic.Entities.Parcel, Services.DTOs.WebhookMessage>().ReverseMap();

            CreateMap<DTOs.Truck, BusinessLogic.Entities.Truck>()
                .IncludeBase<DTOs.Hop, BusinessLogic.Entities.Hop>()
                .ReverseMap();

            CreateMap<DTOs.Warehouse, BusinessLogic.Entities.Warehouse>()
                .IncludeBase<DTOs.Hop, BusinessLogic.Entities.Hop>()
                .ReverseMap();

            CreateMap<DTOs.WarehouseNextHops, BusinessLogic.Entities.WarehouseNextHops>().ReverseMap();

            //Resolver
            CreateMap<BusinessLogic.Entities.Truck, DataAccess.Entities.Truck>()
                .IncludeBase<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom<GeoJsonResolver>());
            //Resolver
            CreateMap<DataAccess.Entities.Truck, BusinessLogic.Entities.Truck>()
                .IncludeBase<DataAccess.Entities.Hop, BusinessLogic.Entities.Hop>()
                .ForMember(dest => dest.RegionGeoJson, opt => opt.MapFrom<GeoJsonResolver>());

            //ResolverTWH
            CreateMap<BusinessLogic.Entities.Transferwarehouse, DataAccess.Entities.Transferwarehouse>()
                .IncludeBase<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom<GeoJsonTWarehouseResolver>());
            //ResolverTWH
            CreateMap<DataAccess.Entities.Transferwarehouse, BusinessLogic.Entities.Transferwarehouse>()
                .IncludeBase<DataAccess.Entities.Hop, BusinessLogic.Entities.Hop>()
                .ForMember(dest => dest.RegionGeoJson, opt => opt.MapFrom<GeoJsonTWarehouseResolver>());



            //Converter
            CreateMap<BusinessLogic.Entities.GeoCoordinate, Point>()
                .ConvertUsing(new GeoPointConverter());

            CreateMap<Point, BusinessLogic.Entities.GeoCoordinate>()
                .ConvertUsing(new GeoPointConverter());


            CreateMap<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>().ReverseMap();
        }




    }
}
