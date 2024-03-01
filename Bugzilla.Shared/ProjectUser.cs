using Bugzilla.Shared;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bugzilla.Shared
{
    public class ProjectUser
    {
            [Key]
            public int Id { get; set; }
            public Project Project { get; set; }
            public ApplicationUser User { get; set; }
            public int ProjectId { get; set; }
            public string UserId { get; set; }

    }
}
