using Microsoft.AspNetCore.Http;
using TechLoop.Authenticates;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Services;
using TechLoop.Models;
using Microsoft.AspNetCore.Authorization;
using TechLoop.DTOs;

namespace TechLoop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthentication _authentication;
        public UserController(IUserService userService, IAuthentication authentication)
        {
            _userService = userService;
            _authentication = authentication;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(User user)
        //{
        //    try
        //    {
        //        if (user.Role != "Instructor" && user.Role != "Student")
        //        {
        //            return BadRequest("You Must have to choose a role!");
        //        }
        //        await _userService.AddUserAsync(user);
        //        return Ok();
        //    }
        //    // let's create a exception here guys
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                if (dto.Role != "Instructor" && dto.Role != "Student")
                    return BadRequest("Role must be either 'Instructor' or 'Student'.");
                var newUser = new User
                {
                    Name = dto.Name,
                    Role = dto.Role,
                    Email = dto.Email,
                    Password = dto.Password
                };

                await _userService.AddUserAsync(newUser);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }


        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet("get-by-id/{userId}")]
        public async Task<ActionResult<User>> GetUsersById([FromRoute]int userId)
        {
            try
            {
                var user = await _userService.GetUsersByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            //Here i have to mention an Explanination 
            var token = _authentication.Authenticate(user.Email, user.Password , user.Role);
            return Ok(new { token });
        }

        //[HttpPut("profile")]
        //public async Task<IActionResult> UpdateProfile(User user)
        //{
        //    try
        //    {
        //        var User = await _userService.GetUsersByIdAsync(user.UserId);
        //        if (User == null)
        //        {
        //            return BadRequest("User Details Not Found To Update");
        //        }
        //        if (User.Role != "Instructor" && User.Role != "Studnet")
        //        {
        //            return BadRequest("You have to choose a Role");


        //        }
        //        User.Name = user.Name;
        //        User.Email = user.Email;
        //        User.Role = user.Role;
        //        if (!string.IsNullOrEmpty(user.Password))
        //        {
        //            User.Password = user.Password;
        //        }
        //        await _userService.UpdateUserAsync(User);

        //        return Ok("User Detailes Updated Successfully");
        //    }
        //    catch (Exception ex) 
        //    { 
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);    
        //    }
        //    //return Ok(user);
        //}
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            try
            {
                var user = await _userService.GetUsersByIdAsync(dto.UserId);
                if (user == null)
                {
                    return BadRequest("User details not found to update");
                }

                if (dto.Role != "Instructor" && dto.Role != "Student")
                {
                    return BadRequest("You have to choose a valid role");
                }

                user.Name = dto.Name;
                user.Email = dto.Email;
                user.Role = dto.Role;

                if (!string.IsNullOrEmpty(dto.Password))
                {
                    user.Password = dto.Password;
                }

                await _userService.UpdateUserAsync(user);
                return Ok("User details updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var User = await _userService.GetUsersByIdAsync(id);
                if (User == null)
                {
                    return BadRequest("User Not Found with that details");
                }
                await _userService.DeleteUserAsync(id);
                return Ok("User Details were deleted successfully");
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [HttpGet("GetByEmail/{Email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string Email)
        {
            try
            {
                var user = await _userService.GetUsersByEmailAsync(Email);
                if (user == null) 
                {
                    return BadRequest("User Not Found");  
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
