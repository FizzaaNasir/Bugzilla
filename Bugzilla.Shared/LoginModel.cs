using System.ComponentModel.DataAnnotations;

namespace Bugzilla.Shared
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email field can not be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field can not be empty")]
        public string Password { get; set; }
    }
}
