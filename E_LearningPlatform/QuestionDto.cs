using System.ComponentModel.DataAnnotations;

namespace E_LearningPlatform
{
    public class QuestionDto
    {
        [Key]
        public int QuestionId { get; set; }
        public int AssessmentId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string CorrectAnswer { get; set; }

        public List<OptionDto>? Options { get; set; }
    }
}