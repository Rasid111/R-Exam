using System.ComponentModel.DataAnnotations;

namespace R_Exam.MVC.Models
{
    public class QuestionDetailsViewModel
    {
        public QuestionDetailsViewModel()
        {

        }
        public long Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        [MaxLength(50)]
        public string CorrectAnswerTitle { get; set; }
    }
}
