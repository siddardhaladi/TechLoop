using System.ComponentModel.DataAnnotations;

namespace TechLoop.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name should not exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Student|Instructor", ErrorMessage = "Role must be either 'Student' or 'Instructor'")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
