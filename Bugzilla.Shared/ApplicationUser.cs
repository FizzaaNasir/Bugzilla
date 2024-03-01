using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugzilla.Shared
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ProjectUser> Project_User { get; set; }
    }
}
