using E_LearningPlatform.Models;
using E_LearningPlatform.Repository;

namespace E_LearningPlatform.services
{


    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
                return null;

            return new QuestionDto
            {
                QuestionId = question.QuestionId,
                AssessmentId = question.AssessmentId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                CorrectAnswer = question.CorrectAnswer,
                Options = question.Options.Select(o => new OptionDto
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };
        }

        public async Task<List<QuestionDto>> GetQuestionsByAssessmentIdAsync(int assessmentId)
        {
            var questions = await _questionRepository.GetByAssessmentIdAsync(assessmentId);
            return questions.Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                AssessmentId = q.AssessmentId,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options.Select(o => new OptionDto
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            }).ToList();
        }

        public async Task AddQuestionAsync(QuestionDto dto)
        {
            var question = new Question
            {
                AssessmentId = dto.AssessmentId,
                QuestionText = dto.QuestionText,
                QuestionType = dto.QuestionType,
                CorrectAnswer = dto.CorrectAnswer,
                Options = dto.Options != null
                ? dto.Options.Select(o => new Option
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
                : new List<Option>()
            };

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            await _questionRepository.DeleteAsync(id);
            await _questionRepository.SaveAsync();
        }
    }
}
