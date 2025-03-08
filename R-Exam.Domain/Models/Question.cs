#pragma warning disable IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
namespace R_Exam.Domain.Models
#pragma warning restore IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
{
    public class Question
    {
        public Question()
        {
            Answers = [];
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        public string CorrectAnswerTitle { get; set; }

    }
}
