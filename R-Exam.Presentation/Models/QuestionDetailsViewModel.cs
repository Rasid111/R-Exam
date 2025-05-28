using System.ComponentModel.DataAnnotations;

namespace R_Exam.Presentation.Models
{
    public class QuestionDetailsViewModel
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public required string Title { get; set; }
        public List<string> Answers { get; set; } = [];
        [MaxLength(50)]
        public required string CorrectAnswerTitle { get; set; }
    }
}
