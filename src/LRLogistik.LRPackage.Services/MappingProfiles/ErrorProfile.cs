using AutoMapper;


namespace LRLogistik.LRPackage.Services.MappingProfiles
{
    public class ErrorProfile : Profile
    {

        public ErrorProfile()
        {
            CreateMap<BusinessLogic.Entities.Error, DTOs.Error>().ReverseMap();

        }


    }
}