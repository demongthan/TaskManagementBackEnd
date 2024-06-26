using AutoMapper;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;
using TaskManagement.BusinessLogicLayer.Request;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.RepositoryParameters;

namespace TaskManagement.BusinessLogicLayer.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemParameter, SystemParameterDto>().ReverseMap();
            CreateMap<SystemParameterCreateDto, SystemParameter>().ReverseMap();

            CreateMap<TaskItem, TaskDto>().ReverseMap();
            CreateMap<TaskCreateDto, TaskItem>().ReverseMap();
            CreateMap<TaskUpdateDto, TaskItem>().ReverseMap();

            CreateMap<TaskRP, TaskRequestParameter>().ReverseMap();
        }
    }
}
