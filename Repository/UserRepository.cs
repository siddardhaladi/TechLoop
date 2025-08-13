using Microsoft.EntityFrameworkCore;
using TechLoop.Data;
using TechLoop.Models;

namespace TechLoop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TechLoopDbContext _context;
        public UserRepository(TechLoopDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync(); 
        }

        //public TechLoopDbContext Get_context()
        //{
        //    return _context;
        //}

        public async Task<User> GetUsersByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<User> GetUsersByEmailAsync(string email)
        {
            return await _context.Users.FirstAsync(x => x.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existing != null)
                throw new Exception("A user with this email already exists.");

           
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            
            if (user.Role == "Student")
            {
                var student = new Student { UserId = user.UserId };
                await _context.Students.AddAsync(student);
            }
            else if (user.Role == "Instructor")
            {
                var instructor = new Instructor { UserId = user.UserId };
                await _context.Instructors.AddAsync(instructor);
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser == null)
                throw new Exception("User not found");


            var emailOwner = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.UserId != user.UserId);
            if (emailOwner != null)
                throw new Exception("Email already exists for another user.");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            if (!string.IsNullOrEmpty(user.Password))
                existingUser.Password = user.Password;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) 
            {
                _context.Users.Remove(user);      
                await _context.SaveChangesAsync();
            }
        }
    }
}
