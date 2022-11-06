using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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