using MediatR;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionCreateRequestDto : IRequest<QuestionCreateResponseDto>
    {
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswerTitle { get; set; }
    }
}
