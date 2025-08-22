﻿using E_LearningPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_LearningPlatform.Repository;
using E_LearningPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using E_LearningPlatform.Exceptions;

namespace E_LearningPlatform.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentServices _enrollmentService;

        public EnrollmentController(IEnrollmentServices enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }


        // GET: api/enrollment
       // [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // GET: api/enrollment/{id}
        //[Authorize(Roles = "Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                if (enrollment == null)
                {
                    return NotFound();
                }
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // POST: api/enrollment
        //[Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                await _enrollmentService.AddEnrollmentAsync(enrollment);
                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentId }, enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        // DELETE: api/enrollment/{id}
       // [Authorize(Roles = "Student")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                if (enrollment == null)
                {
                    return NotFound();
                }
                await _enrollmentService.DeleteEnrollmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       // [Authorize(Roles = "Student,Instructor")]
        [HttpPut("{id}/progress")]
        public async Task<IActionResult> UpdateProgress(int id, [FromBody] double progress)
        {
            try
            {
                await _enrollmentService.UpdateProgressAsync(id, progress);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("student/{studentId}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetEnrolledCourses(int studentId)
        {
            try
            {
                var courses = await _enrollmentService.GetAllEnrollmentByStudentIdAsync(studentId);
                return Ok(courses);
            }
            catch (DetailsNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}