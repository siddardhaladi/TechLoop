using E_LearningPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_LearningPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_LearningPlatform.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_LearningPlatform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _service;

        public AssessmentController(IAssessmentService service)
        {
            _service = service;
        }
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetAssessmentIdByCourseId(int courseId)
        {
            try
            {
                var assessment = await _service.GetAssessmentByCourseIdAsync(courseId);

                if (assessment == null)
                {
                    return NotFound();
                }

                return Ok(new { AssessmentId = assessment.AssessmentId });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while getting the assessment.");

                return StatusCode(500, "Internal server error");
            }
        }

         [Authorize(Roles = "Student,Instructor")]
        // GET: api/Assessments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessmentsAsync()
        {
            try
            {
                var assessments = await _service.GetAllAsync();
                return Ok(assessments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Student,Instructor")]
        // GET: api/Assessments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessmentAsync(int id)
        {
            try
            {
                var assessment = await _service.GetByIdAsync(id);

                if (assessment == null)
                {
                    throw new DetailsNotFoundException($"Assessment with id {id} does not exist");
                }

                return Ok(assessment);
            }
            catch (DetailsNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       [Authorize(Roles = "Instructor")]
        // POST: api/Assessments
        [HttpPost]
        public async Task<ActionResult> PostAssessmentAsync(Assessment assessment)
        {
            try
            {
                if (assessment.Type != "Assessment" && assessment.Type != "Quiz")
                {
                    return BadRequest("Assessment Type must be either 'Assessment' or 'Quiz'");
                }
                await _service.AddAsync(assessment);
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

        [Authorize(Roles = "Instructor")]
        // PUT: api/Assessments/5
        [HttpPut("{Assessmentid}")]
        public async Task<IActionResult> PutAssessmentAsync(int id, Assessment assessment)
        {
            if (id != assessment.AssessmentId)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(assessment);
                return NoContent();
            }
            catch (DetailsNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Instructor")]
        // DELETE: api/Assessments/5
        [HttpDelete("{Assessmentid}")]
        public async Task<IActionResult> DeleteAssessmentAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (DetailsNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}