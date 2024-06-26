namespace TaskManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class TaskRP : BaseRP
    {
        private bool? isCompleted;
        public TaskRP(int pageNumber, int pageSize, string orderBy, bool? isCompleted) : base(pageNumber, pageSize, orderBy)
        {
            this.IsCompleted = isCompleted;
        }

        public bool? IsCompleted { get => isCompleted; set => isCompleted = value; }
    }
}
