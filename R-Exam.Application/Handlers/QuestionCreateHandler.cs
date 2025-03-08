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

namespace R_Exam.Application.Handlers
{
    public class QuestionCreateHandler(IQuestionService service, IMapper mapper) : IRequestHandler<QuestionCreateRequestDto, QuestionCreateResponseDto>
    {
        private readonly IQuestionService service = service;
        private readonly IMapper mapper = mapper;

        public async Task<QuestionCreateResponseDto> Handle(QuestionCreateRequestDto request, CancellationToken cancellationToken)
        {
            var question = this.mapper.Map<Question>(request);
            var questionId = await this.service.Create(question);
            var response = new QuestionCreateResponseDto(questionId);
            return response;
        }
    }
}
