using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using TaskManagement.DataAccessLayer.Repository.AstractClass;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;

namespace TaskManagement.BusinessLogicLayer.ActionFilters
{
    public class ValidateTaskExistsAttribute : IAsyncActionFilter
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateTaskExistsAttribute(ITaskRepository taskRepository, ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _taskRepository = taskRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            if (method.Equals("POST"))
            {
                var param = (TaskCreateDto)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

                var systemParameter = await _taskRepository.GetTaskByTitleAsyn(param.Title, true);

                if (systemParameter != null)
                {
                    _loggerManager.LogInfo($"System Parameter code with code: {param.Title} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    await next();
                }
            }
            else
            {
                var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
                var id = (Guid)context.ActionArguments["id"];
                var systemParameter = await _taskRepository.GetTaskByIdAsyn(id, trackChanges);

                if (systemParameter == null)
                {
                    _loggerManager.LogInfo($"System Parameter with id: {id} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.HttpContext.Items.Add("task", systemParameter);
                    await next();
                }
            }
        }
    }
}
