using MediatR;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionCreateRequestDto : IRequest<QuestionCreateResponseDto>
    {
        public required string Title { get; set; }
        public List<string> Answers { get; set; } = [];
        public required string CorrectAnswerTitle { get; set; }
    }
}
