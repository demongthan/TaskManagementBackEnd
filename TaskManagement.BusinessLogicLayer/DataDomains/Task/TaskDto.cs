namespace TaskManagement.BusinessLogicLayer.DataDomains.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }
    }
}
