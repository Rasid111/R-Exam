using AutoMapper;
using MediatR;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Services;

namespace R_Exam.Application.Handlers.Question
{
    public class QuestionGetAllHandler(IQuestionService service) : IRequestHandler<QuestionGetAllRequestDto, List<QuestionGetResponseDto>>
    {
        private readonly IQuestionService service = service;

        public async Task<List<QuestionGetResponseDto>> Handle(QuestionGetAllRequestDto request, CancellationToken cancellationToken)
        {
            var questions = await service.Get();
            return questions;
        }
    }
}
