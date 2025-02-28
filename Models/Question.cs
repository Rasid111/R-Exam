namespace Models
{
    using System.ComponentModel.DataAnnotations;
    public class Question
    {
        public Question(long id, string title, List<Answer> answers, string correctAnswerTitle)
        {
            Id = id;
            Title = title;
            Answers = answers;
            CorrectAnswerTitle = correctAnswerTitle;
        }
        public long Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        [MaxLength(50)]
        public string CorrectAnswerTitle { get; set; }

        public Question()
        {

        }
    }
}
