namespace R_Exam.Models
{
    public class Question
    {
        public Question(int id, string title, string[] answers, string rightAnswer) {
            Id = id;
            Title = title;
            Answers = answers;
            RightAnswer = rightAnswer;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string[] Answers { get; set; }
        public string RightAnswer { get; set; }
    }
}
