using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using TaskManagement.BusinessLogicLayer.ActionFilters;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.BusinessLogicLayer.Services.AstractClass;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemParameterController : ControllerBase
    {
        private readonly ISystemParameterService _systemParameterService;
        private readonly IMapper _mapper;

        public SystemParameterController(ISystemParameterService systemParameterService, IMapper mapper)
        {
            _mapper = mapper;
            _systemParameterService = systemParameterService;
        }

        [HttpPost("CreateSystemParameter")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateSystemParameterExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateSystemParameter([FromBody] SystemParameterCreateDto systemParameterCreateDto, [FromQuery] string? fileds)
        {
            var systemParameterEntity = _mapper.Map<SystemParameter>(systemParameterCreateDto);

            return await _systemParameterService.CreateSystemParameter(systemParameterEntity, fileds);
        }
    }
}
