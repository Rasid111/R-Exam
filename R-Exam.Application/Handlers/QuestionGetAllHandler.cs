using AutoMapper;
using MediatR;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Services;

namespace R_Exam.Application.Handlers
{
    internal class QuestionGetAllHandler(IQuestionService service, IMapper mapper) : IRequestHandler<QuestionGetAllRequestDto, List<QuestionGetResponseDto>>
    {
        private readonly IQuestionService service = service;
        private readonly IMapper mapper = mapper;

        public async Task<List<QuestionGetResponseDto>> Handle(QuestionGetAllRequestDto request, CancellationToken cancellationToken)
        {
            var questions = await this.service.Get();
            var questionsDto =  questions.Select(question => this.mapper.Map<QuestionGetResponseDto>(question)).ToList();
            return questionsDto;
        }
    }
}
