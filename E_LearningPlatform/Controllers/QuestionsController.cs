using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using E_LearningPlatform.Exceptions;
using E_LearningPlatform.services;
using E_LearningPlatform;

namespace ElearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionsController(IQuestionService service)
        {
            _service = service;
        }

        // GET: api/Questions/GetByAssessment
        [HttpGet("GetByAssessment/{assessmentId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetByAssessmentAsync(int assessmentId)
        {
            try
            {
                var questions = await _service.GetQuestionsByAssessmentIdAsync(assessmentId);
                return Ok(questions);
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

        // GET: api/Questions/Get/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionAsync(int id)
        {
            try
            {
                var question = await _service.GetQuestionByIdAsync(id);
                if (question == null)
                    throw new DetailsNotFoundException($"Question with id {id} does not exist");

                return Ok(question);
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

        // POST: api/Questions/Post
        [HttpPost("Post")]
        public async Task<ActionResult> PostQuestionAsync(QuestionDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid question data");

                await _service.AddQuestionAsync(dto);
                return Ok("Question added successfully.");
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

        // DELETE: api/Questions/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(int id)
        {
            try
            {
                await _service.DeleteQuestionAsync(id);
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