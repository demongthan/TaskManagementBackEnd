using System.Dynamic;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ISystemParameterService
    {
        Task<ApiReponse<ExpandoObject>> CreateSystemParameter(SystemParameter systemParameterEntity, string fileds);
    }
}
