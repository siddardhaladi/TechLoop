using TechLoop.Models;

namespace TechLoop.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUsersByIdAsync(int id);
        Task<User> GetUsersByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int Userid);
    }
}
