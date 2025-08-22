using E_LearningPlatform.Models;

namespace E_LearningPlatform.Repository
{
    public interface IQuestionRepository
    {

        Task<Question> GetByIdAsync(int id);
        Task<List<Question>> GetByAssessmentIdAsync(int assessmentId);
        Task AddAsync(Question question);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
