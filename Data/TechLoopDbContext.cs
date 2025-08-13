using Microsoft.EntityFrameworkCore;
using TechLoop.Models;

namespace TechLoop.Data
{
    public class TechLoopDbContext : DbContext
    {
        public TechLoopDbContext(DbContextOptions<TechLoopDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
