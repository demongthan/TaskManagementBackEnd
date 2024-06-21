using Microsoft.EntityFrameworkCore;
using TaskManagement.DataAccessLayer.ApplicationDbContext;
using TaskManagement.DataAccessLayer.Common;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.AstractClass;
using TaskManagement.DataAccessLayer.Repository.RepositoryExtensions;
using TaskManagement.DataAccessLayer.Repository.RepositoryParameters;

namespace TaskManagement.DataAccessLayer.Repository
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        private readonly DataDbContext _dbContext;

        public TaskRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateTaskAsyn(TaskItem taskItem) => Create(taskItem);

        public async Task<PagedList<TaskItem>> GetAllSystemParameterAsyn(TaskRP taskRP, bool trackChanges)
        {
            var tasks = await FindAll(trackChanges).PagedTasks(taskRP.PageNumber, taskRP.PageSize).Search(taskRP.SearchTerm).ToListAsync();
            var count = await FindAll(trackChanges).Search(taskRP.SearchTerm).CountAsync();
            var metaData = new MetaData(taskRP.PageSize, taskRP.PageNumber, count);

            return PagedList<TaskItem>.ToPagedList(tasks, metaData);
        }

        public async Task<TaskItem> GetTaskByTitleAsyn(string title, bool trackChanges) => await FindByCondition(p => p.Title.Equals(title), trackChanges).SingleOrDefaultAsync();

        public async Task<TaskItem> GetTaskByIdAsyn(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
