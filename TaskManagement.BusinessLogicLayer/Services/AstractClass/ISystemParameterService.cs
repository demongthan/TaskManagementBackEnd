using System.Dynamic;

namespace TaskManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ISystemParameterService
    {
        Task<ApiReponse<ExpandoObject>> CreateSystemParameter(SystemParameter systemParameterEntity, string fileds);
    }
}
