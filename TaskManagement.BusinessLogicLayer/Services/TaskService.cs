using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using TaskManagement.BusinessLogicLayer.Common;
using TaskManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;
using TaskManagement.BusinessLogicLayer.Request;
using TaskManagement.BusinessLogicLayer.Services.AstractClass;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.AstractClass;
using TaskManagement.DataAccessLayer.Repository.RepositoryParameters;
using TaskManagement.DataAccessLayer.UnitOfWork.AstractClass;

namespace TaskManagement.BusinessLogicLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDataShaper<TaskDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;
        private readonly ISystemParameterRepository _systemParameterRepository;

        public TaskService(ITaskRepository taskRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDataShaper<TaskDto> dataShaper,
            ILoggerManager loggerManager,
            ISystemParameterRepository systemParameterRepository)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _loggerManager = loggerManager;
            _systemParameterRepository = systemParameterRepository;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateTask(TaskItem taskEntity, string? fileds)
        {
            taskEntity.IsCompleted = false;

            _taskRepository.CreateTaskAsyn(taskEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var taskReturn = _mapper.Map<TaskDto>(taskEntity);
                var result = _dataShaper.ShapeData(taskReturn, fileds);
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_MESSAGE_CREATE_TASK_SUCCESFULL, false);

                _loggerManager.LogInfo(string.Format("Task create successfully!", taskReturn.Id));

                return new ApiReponse<ExpandoObject>(systemParameter.Content, result);
            }
            else
            {
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_MESSAGE_CREATE_TASK_FAIL, false);

                return new ApiReponse<ExpandoObject>(systemParameter.Content);
            }
        }

        public async Task<ApiReponse<IEnumerable<ExpandoObject>>> GetAllTask(TaskRequestParameter taskRequestParameter)
        {
            var taskRP = _mapper.Map<TaskRP>(taskRequestParameter);
            var tasksFromDb = await _taskRepository.GetAllTaskAsyn(taskRP, false);
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasksFromDb);
            var result = _dataShaper.ShapeData(tasksDto, taskRequestParameter.Fields);

            return new ApiReponse<IEnumerable<ExpandoObject>>(null, result);
        }

        public async Task<ApiReponse<ExpandoObject>> GetTaskById(Guid id, string? fileds)
        {
            var taskFromDb = await _taskRepository.GetTaskByIdAsyn(id, false);
            var taskDto = _mapper.Map<TaskDto>(taskFromDb);
            var result = _dataShaper.ShapeData(taskDto, fileds);

            return new ApiReponse<ExpandoObject>(null, result);
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateTask(TaskItem taskEntity, string fileds)
        {
            taskEntity.UpdateAt = DateTime.UtcNow;
            _taskRepository.UpdateTaskAsyn(taskEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var taskReturn = _mapper.Map<TaskDto>(taskEntity);
                var result = _dataShaper.ShapeData(taskReturn, fileds);

                return new ApiReponse<ExpandoObject>("successfully", result);
            }
            else
            {
                return new ApiReponse<ExpandoObject>("Failed");
            }
        }

        public async Task<ApiReponse<bool>> DeleteTask(TaskItem taskEntity)
        {
            _taskRepository.DeleteTaskAsyn(taskEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ApiReponse<bool>("Successfull", true);
            }
            else
            {
                return new ApiReponse<bool>("Failed");
            }
        }
    }
}
