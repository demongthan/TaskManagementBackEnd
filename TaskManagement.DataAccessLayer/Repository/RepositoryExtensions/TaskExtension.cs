using TaskManagement.DataAccessLayer.Common.Utility;
using TaskManagement.DataAccessLayer.DataModels;
using System.Linq.Dynamic.Core;

namespace TaskManagement.DataAccessLayer.Repository.RepositoryExtensions
{
    public static class TaskExtension
    {
        public static IQueryable<TaskItem> PagedTasks(this IQueryable<TaskItem> tasks, int pageNumber, int pageSize) => tasks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public static IQueryable<TaskItem> Search(this IQueryable<TaskItem> tasks, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return tasks;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return tasks.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<SystemParameter> Sort(this IQueryable<SystemParameter> systemParameters, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return systemParameters.OrderBy(e => e.Code);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SystemParameter>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return systemParameters.OrderBy(e => e.Code);
            return systemParameters.OrderBy(orderQuery);
        }
    }
}
