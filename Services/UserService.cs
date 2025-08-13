using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLoop.Data;
using TechLoop.Models;
using TechLoop.Repository;

namespace TechLoop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TechLoopDbContext _context;

        public UserService(IUserRepository userRepository, TechLoopDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User> GetUsersByIdAsync(int id)
        {
            return await _userRepository.GetUsersByIdAsync(id);
        }

        public async Task<User> GetUsersByEmailAsync(string email)
        {
            return await _userRepository.GetUsersByEmailAsync(email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
            return user; 
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task<User> DeleteUserAsync(int userid)
        {
            var user = await _userRepository.GetUsersByIdAsync(userid);
            if (user == null)
                throw new Exception("User not found");

            await _userRepository.DeleteUserAsync(userid);
            return user;
        }
    }
}
