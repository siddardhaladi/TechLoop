using System.ComponentModel.DataAnnotations;

namespace E_LearningPlatform.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public int AssessmentId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } // "mcq" or "fill"
        public string CorrectAnswer { get; set; }
        public Assessment Assessment { get; set; }

        public virtual ICollection<Option> Options { get; set; }
    }
}

