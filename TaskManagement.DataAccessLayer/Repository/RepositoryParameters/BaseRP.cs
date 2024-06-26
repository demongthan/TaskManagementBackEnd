namespace TaskManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class BaseRP
    {
        private int pageNumber;
        private int pageSize;
        private string orderBy;

        public BaseRP(int pageNumber, int pageSize, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            this.orderBy = orderBy;
        }

        public int PageNumber { get => pageNumber; set => pageNumber = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public string OrderBy { get => orderBy; set => orderBy = value; }
    }
}
