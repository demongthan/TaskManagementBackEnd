using TaskManagement.DataAccessLayer.Common.Utility;
using TaskManagement.DataAccessLayer.DataModels;
using System.Linq.Dynamic.Core;

namespace TaskManagement.DataAccessLayer.Repository.RepositoryExtensions
{
    public static class TaskExtension
    {
        public static IQueryable<TaskItem> PagedTasks(this IQueryable<TaskItem> tasks, int pageNumber, int pageSize) => tasks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public static IQueryable<TaskItem> Search(this IQueryable<TaskItem> tasks, bool? isCompleted)
        {
            if (isCompleted==null)
                return tasks;

            return tasks.Where(e => e.IsCompleted==isCompleted);
        }

        public static IQueryable<TaskItem> Sort(this IQueryable<TaskItem> tasks, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return tasks.OrderBy(e => e.Title);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SystemParameter>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return tasks.OrderBy(e => e.Title);
            return tasks.OrderBy(orderQuery);
        }
    }
}
