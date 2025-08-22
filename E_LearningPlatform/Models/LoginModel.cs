using System.ComponentModel.DataAnnotations;

namespace E_LearningPlatform.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Student|Instructor", ErrorMessage = "Role must be either 'Student' or 'Instructor'")]
        public string Role { get; set; }

    }
}
