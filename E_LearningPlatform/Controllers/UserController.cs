﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_LearningPlatform.Models;
using E_LearningPlatform.Services;
using System.Threading.Tasks;
using E_LearningPlatform.Exceptions;
using Microsoft.AspNetCore.Http;
using E_LearningPlatform.Authentication;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthentication jwtAuth;

        public UserController(IUserService userService, IAuthentication jwtAuth)
        {
            _userService = userService;
            this.jwtAuth = jwtAuth;
        }

        // Register a new user as Student or Instructor
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            try
            {
                // Validate the role
                if (user.Role != "Instructor" && user.Role != "Student")
                {
                    return BadRequest("Role must be either 'Instructor' or 'Student'");
                }

                await _userService.AddUserAsync(user);
                //return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
                return Ok();
            }
            catch (DetailsAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Get all users
        [HttpGet("GetallUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // Get user by ID
        [HttpGet("Get by Id")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // Login a user
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authentication([FromBody] LoginModel user)
        {
            var token = jwtAuth.Authenticate(user.Email, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        // Update user profile
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(User updatedUser)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(updatedUser.UserID);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Validate the role
                if (updatedUser.Role != "Instructor" && updatedUser.Role != "Student")
                {
                    return BadRequest("Role must be either 'Instructor' or 'Student'");
                }

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Role = updatedUser.Role;
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    user.Password = updatedUser.Password;
                }

                await _userService.UpdateUserAsync(user);
                return Ok("User profile updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Delete user
        [HttpDelete("Delete by Id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                await _userService.DeleteUserAsync(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetbyEmail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}