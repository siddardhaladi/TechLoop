using TechLoop.Models;
namespace TechLoop.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUsersByIdAsync(int id);
        Task<User> GetUsersByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(int userid);
    }
}
