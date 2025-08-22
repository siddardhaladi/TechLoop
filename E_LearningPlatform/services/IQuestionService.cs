namespace E_LearningPlatform.services
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<List<QuestionDto>> GetQuestionsByAssessmentIdAsync(int assessmentId);
        Task AddQuestionAsync(QuestionDto dto);
        Task DeleteQuestionAsync(int id);


    }
}
