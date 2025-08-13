using System.ComponentModel.DataAnnotations;

namespace TechLoop.DTOs
{
    public class UpdateUserProfileDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("Student|Instructor")]
        public string Role { get; set; }

        public string? Password { get; set; }
    }
}

