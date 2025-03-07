using System.ComponentModel.DataAnnotations;

namespace R_Exam.MVC.ViewModels
{
    public class Question(long id, string title, List<Answer> answers, string correctAnswerTitle)
    {
        public long Id { get; set; } = id;
        [MaxLength(50)]
        public string Title { get; set; } = title;
        public List<Answer> Answers { get; set; } = answers;
        [MaxLength(50)]
        public string CorrectAnswerTitle { get; set; } = correctAnswerTitle;
        public Question() : this(0, null, null, null)
        {

        }
    }
}
