namespace TaskManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class SystemParameterRP : BaseRP
    {
        private string searchTerm;
        public SystemParameterRP(int pageNumber, int pageSize, string orderBy, string SearchTerm) : base(pageNumber, pageSize, orderBy)
        {
            this.searchTerm = SearchTerm;
        }

        public string SearchTerm { get => searchTerm; set => searchTerm = value; }
    }
}
