using AutoMapper;
using MediatR;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Services;
using R_Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Handlers.Question
{
    public class QuestionGetHandler(IQuestionService service, IMapper mapper) : IRequestHandler<QuestionGetRequestDto, QuestionGetResponseDto>
    {
        private readonly IQuestionService service = service;
        private readonly IMapper mapper = mapper;

        public async Task<QuestionGetResponseDto> Handle(QuestionGetRequestDto request, CancellationToken cancellationToken)
        {
            var question = await service.Get(request.Id);
            var questionDto = mapper.Map<QuestionGetResponseDto>(question);
            return questionDto;
        }
    }
}
