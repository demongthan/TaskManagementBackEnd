namespace TaskManagement.DataAccessLayer.DataModels
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
