using System.ComponentModel.DataAnnotations;

namespace Bugzilla.Shared
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email field can not be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
      
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field can not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password field can not be empty")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role can not be empty")]
        public string Role { get; set; }
    }
}
