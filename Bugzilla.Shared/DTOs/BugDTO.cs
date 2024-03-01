using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugzilla.Shared.DTOs
{
    public class BugDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public string AssigneeId { get; set; }
        public string CreaterId { get; set; }
    }
}
