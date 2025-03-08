using System.ComponentModel.DataAnnotations;

namespace R_Exam.MVC.Models
{
    public class QuestionCreateViewModel
    {
        public QuestionCreateViewModel()
        {

        }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Answers { get; set; }
        [MaxLength(50)]
        public string CorrectAnswerTitle { get; set; }
    }
}
