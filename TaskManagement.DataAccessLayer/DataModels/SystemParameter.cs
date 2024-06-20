namespace TaskManagement.DataAccessLayer.DataModels
{
    public class SystemParameter
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
