using MediatR;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionUpdateRequestDto : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswerTitle { get; set; }
    }
}
