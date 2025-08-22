using E_LearningPlatform.Data;
using E_LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_LearningPlatform.Repository
{

    public class QuestionRepository : IQuestionRepository
    {
        private readonly ELearningDbContext _context;

        public QuestionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.QuestionId == id);
        }

        public async Task<List<Question>> GetByAssessmentIdAsync(int assessmentId)
        {
            return await _context.Questions
                .Where(q => q.AssessmentId == assessmentId)
                .Include(q => q.Options)
                .ToListAsync();
        }

        public async Task AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _context.Questions.Include(q => q.Options).FirstOrDefaultAsync(q => q.QuestionId == id);
            if (question != null)
            {
                _context.Options.RemoveRange(question.Options);
                _context.Questions.Remove(question);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}