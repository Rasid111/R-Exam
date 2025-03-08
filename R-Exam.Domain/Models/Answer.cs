namespace R_Exam.Domain.Models
{
    public class Answer
    {
        public Answer()
        {

        }
        public long Id { get; set; }
        public string Title { get; set; }
        public long QuestionId { get; set; }
    }
}
