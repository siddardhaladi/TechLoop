using E_LearningPlatform.services;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

namespace E_LearningPlatform.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class RoleController : ControllerBase

    {

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)

        {

            _roleService = roleService;

        }



        [HttpGet("student/{userId}")]

        public async Task<IActionResult> GetStudentIdByUserId(int userId)

        {

            var studentId = await _roleService.GetStudentIdByUserIdAsync(userId);

            if (studentId == 0)

            {

                return NotFound("Student not found");

            }

            return Ok(studentId);

        }


        [HttpGet("instructor/{userId}")]

        public async Task<IActionResult> GetInstructorIdByUserId(int userId)

        {

            var instructorId = await _roleService.GetInstructorIdByUserIdAsync(userId);

            if (instructorId == 0)

            {

                return NotFound("Instructor not found");

            }

            return Ok(instructorId);

        }

    }

}

