using System.Dynamic;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.Request;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ITaskService
    {
        Task<ApiReponse<ExpandoObject>> CreateTask(TaskItem taskEntity, string fileds);

        Task<ApiReponse<IEnumerable<ExpandoObject>>> GetAllTask(TaskRequestParameter taskRequestParameter);

        Task<ApiReponse<bool>> DeleteTask(TaskItem taskEntity);

        Task<ApiReponse<ExpandoObject>> UpdateTask(TaskItem taskEntity, string fileds);

        Task<ApiReponse<ExpandoObject>> GetTaskById(Guid id, string? fileds);
    }
}
