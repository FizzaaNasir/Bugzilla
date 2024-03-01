using System.ComponentModel.DataAnnotations;
//using static Bugzilla.Client.Pages.RegisterBase;

namespace Bugzilla.Client.Model
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string[] Roles = new[] { "Manager", "Developer", "QA" };
        public  string Role { get; set; }
    }
}

