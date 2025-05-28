namespace R_Exam.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<Answer> Answers { get; set; } = [];
        public required string CorrectAnswerTitle { get; set; }

    }
}
