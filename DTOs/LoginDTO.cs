using System.ComponentModel.DataAnnotations;

namespace TechLoop.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Student|Instructor", ErrorMessage = "Role must be either 'Student' or 'Instructor'")]
        public string Role { get; set; }
    }
}
