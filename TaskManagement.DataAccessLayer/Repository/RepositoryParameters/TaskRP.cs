namespace TaskManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class TaskRP : BaseRP
    {
        public TaskRP(int pageNumber, int pageSize, string orderBy, string searchTerm) : base(pageNumber, pageSize, orderBy, searchTerm)
        {

        }
    }
}
