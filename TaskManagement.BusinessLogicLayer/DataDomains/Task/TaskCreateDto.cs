using System.ComponentModel.DataAnnotations;

namespace TaskManagement.BusinessLogicLayer.DataDomains.Task
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Title Task is a required field.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the Title is 1000 characters.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public bool IsImportant { get; set; }
    }
}
