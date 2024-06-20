using TaskManagement.DataAccessLayer.Common;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.RepositoryParameters;

namespace TaskManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ISystemParameterRepository
    {
        Task<PagedList<SystemParameter>> GetAllSystemParameterAsyn(SystemParameterRP systemParameterRP, bool trackChanges);

        void CreateSystemParameterAsyn(SystemParameter systemParameter);

        Task<SystemParameter> GetSystemParameterAsyn(Guid id, bool trackChanges);

        Task<SystemParameter> GetSystemParameterAsynByCode(string code, bool trackChanges);
    }
}
