using AutoMapper;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.BusinessLogicLayer.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemParameter, SystemParameterDto>().ReverseMap();
            CreateMap<SystemParameterCreateDto, SystemParameter>().ReverseMap();
        }
    }
}
