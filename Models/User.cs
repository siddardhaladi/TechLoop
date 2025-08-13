using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLoop.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name should not exceed 100 charcters")]
        public string? Name { get; set; }
        [Required]
        [RegularExpression("Student|Instructor", ErrorMessage = "You should give yourself a role")]
        public string? Role { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; } 

        public ICollection<Student>? Students{get;set;}
        public ICollection<Instructor>? Instructors { get; set; }
    }
}
