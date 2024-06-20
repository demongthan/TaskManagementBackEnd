using TaskManagement.DataAccessLayer.ApplicationDbContext;
using TaskManagement.DataAccessLayer.Common;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.AstractClass;

namespace TaskManagement.DataAccessLayer.Repository
{
    public class SystemParameterRepository : BaseRepository<SystemParameter>, ISystemParameterRepository
    {
        private readonly DataDbContext _dbContext;

        public SystemParameterRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<SystemParameter>> GetAllSystemParameterAsyn(SystemParameterRP systemParameterRP, bool trackChanges)
        {
            var systemParameters = await FindAll(trackChanges).PagedSystemParameter(systemParameterRP.PageNumber, systemParameterRP.PageSize).Search(systemParameterRP.SearchTerm).ToListAsync();

            var count = await FindAll(trackChanges).Search(systemParameterRP.SearchTerm).CountAsync();

            var metaData = new MetaData(systemParameterRP.PageSize, systemParameterRP.PageNumber, count);

            return PagedList<SystemParameter>.ToPagedList(systemParameters, metaData);
        }

        public async Task<SystemParameter> GetSystemParameterAsyn(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<SystemParameter> GetSystemParameterAsynByCode(string code, bool trackChanges) => await FindByCondition(p => p.Code.Equals(code), trackChanges).SingleOrDefaultAsync();

        public void CreateSystemParameterAsyn(SystemParameter systemParameter) => Create(systemParameter);

        public void UpdateSystemParameterAsyn(SystemParameter systemParameter) => Update(systemParameter);

        public void DeleteSystemParameterAsyn(SystemParameter systemParameter) => Delete(systemParameter);
    }
