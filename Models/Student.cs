using System.ComponentModel.DataAnnotations;

namespace TechLoop.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public int UserId { get; set; }

        public  User? User { get; set; }

    }
}
