using System.Dynamic;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ITaskService
    {
        Task<ApiReponse<ExpandoObject>> CreateTask(TaskItem taskEntity, string fileds);
    }
}
