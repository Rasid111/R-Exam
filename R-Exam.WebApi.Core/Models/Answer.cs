using System.ComponentModel.DataAnnotations;

namespace R_Exam.WebApi.Core.Models
{
    public class Answer
    {
        public Answer(long id, string title, int questionId)
        {
            Id = id;
            Title = title;
            QuestionId = questionId;
        }
        public long Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public long QuestionId { get; set; }

        public Answer()
        {

        }
    }
}
