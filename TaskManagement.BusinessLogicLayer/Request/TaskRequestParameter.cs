namespace TaskManagement.BusinessLogicLayer.Request
{
    public class TaskRequestParameter:RequestParameter
    {
        public TaskRequestParameter()
        {

        }
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? Fields { get; set; }
    }
}
