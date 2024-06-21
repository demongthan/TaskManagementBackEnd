using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using TaskManagement.BusinessLogicLayer.ActionFilters;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;
using TaskManagement.BusinessLogicLayer.Services.AstractClass;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService,
            IMapper mapper)
        {
            _mapper = mapper;
            _taskService = taskService;
        }

        [HttpPost("CreateTask")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateTask([FromBody] TaskCreateDto taskCreateDto, [FromQuery] string? fileds)
        {
            var taskEntity = _mapper.Map<TaskItem>(taskCreateDto);

            return await _taskService.CreateTask(taskEntity, fileds);
        }
    }
}
