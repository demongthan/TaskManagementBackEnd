using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using TaskManagement.BusinessLogicLayer.ActionFilters;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;
using TaskManagement.BusinessLogicLayer.Request;
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

        [HttpGet("GetAllTask")]
        public async Task<ApiReponse<IEnumerable<ExpandoObject>>> GetAllTask([FromQuery] TaskRequestParameter taskRequestParameter)
        {
            return await _taskService.GetAllTask(taskRequestParameter);
        }

        [HttpPut("UpdateTask/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateTask([FromRoute] Guid id, [FromBody] TaskUpdateDto taskUpdateDto, [FromQuery] string? fileds)
        {
            var taskEntity = HttpContext.Items["task"] as TaskItem;

            _mapper.Map(taskUpdateDto, taskEntity);

            return await _taskService.UpdateTask(taskEntity, fileds);
        }

        [HttpPatch("UpdateTaskWithFiled/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateTaskWithFileds([FromRoute] Guid id, [FromBody] JsonPatchDocument<TaskUpdateDto> patchDocDto, [FromQuery] string? fileds)
        {
            var taskEntity = HttpContext.Items["task"] as TaskItem;

            var taskToPatch = _mapper.Map<TaskUpdateDto>(taskEntity);

            patchDocDto.ApplyTo(taskToPatch, ModelState);

            _mapper.Map(taskToPatch, taskEntity);

            TryValidateModel(taskToPatch);

            _mapper.Map(taskToPatch, taskEntity);

            return await _taskService.UpdateTask(taskEntity, fileds);
        }

        [HttpDelete("DeleteTask/{id}")]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ApiReponse<bool>> DeleteTask([FromRoute] Guid id)
        {
            var taskEntity = HttpContext.Items["task"] as TaskItem;

            return await _taskService.DeleteTask(taskEntity);
        }
    }
}
