using E_LearningPlatform.Models;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using E_LearningPlatform.Repository;
using System.Threading.Tasks;
using E_LearningPlatform.Services;
using E_LearningPlatform.Exceptions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet] 
        //[Authorize]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving courses: {ex.Message}");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Instructor")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Course data is required");
                }
                await _courseService.CreateCourseAsync(course);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating course: {ex.Message}");
            }
        }

        [HttpGet("instructor/{instructorID}")]
        //[Authorize]
        public async Task<IActionResult> GetCoursesByInstructor(int instructorID)
        {
            try
            {
                var courses = await _courseService.GetCoursesByInstructorAsync(instructorID);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving courses: {ex.Message}");
            }
        }

        [HttpGet("{courseId}")]
        //[Authorize]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(courseId);
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving course: {ex.Message}");
            }
        }

        [HttpPut("{courseId}")]
        //[Authorize(Roles = "Instructor")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] Course course)
        {
            try
            {
                await _courseService.UpdateCourseAsync(courseId, course);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating course: {ex.Message}");
            }
        }

        [HttpDelete("{courseId}")]
        //[Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                await _courseService.DeleteCourseAsync(courseId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting course: {ex.Message}");
            }
        }
        [HttpGet("search")]
        //[Authorize]
        public async Task<IActionResult> SearchCoursesByTitle([FromQuery] string title)
        {
            try
            {
                var courses = await _courseService.SearchCoursesByTitleAsync(title);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error searching courses: {ex.Message}");
            }
        }


    }
}