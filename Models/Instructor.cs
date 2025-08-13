using System.ComponentModel.DataAnnotations;

namespace TechLoop.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public int UserId {  get; set; }
        public User? User { get; set; }
    }
}
