using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bugzilla.Shared
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public ICollection<Bug> Bugs { get; set; }
        public ICollection<ProjectUser> Project_User { get; set; }
    }
}
