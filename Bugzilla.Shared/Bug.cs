using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bugzilla.Shared
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title can not be empty")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Deadine can not be empty")]
        public DateTime Deadline { get; set; }
        [Required(ErrorMessage = "Type can not be empty")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Status can not be empty")]
        public string Status { get; set; } = string.Empty;
        [ForeignKey("ProjectId")]
        public Project Project { get; set; } 
        public int ProjectId { get; set; }
        [ForeignKey("CreaterId")]
        public ApplicationUser Creater { get; set; }      
        public string? CreaterId { get; set; }
        [ForeignKey("AssigneeId")]
        public ApplicationUser Assignee { get; set; }
        public string? AssigneeId { get; set; }
    }
}
