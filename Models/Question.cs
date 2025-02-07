namespace R_Exam.Models
{
    public class Question
    {
        public Question(long id, string title, string[] answers, string rightAnswer) {
            Id = id;
            Title = title;
            Answers = answers;
            RightAnswer = rightAnswer;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string[] Answers { get; set; }
        public string RightAnswer { get; set; }
    }
}
