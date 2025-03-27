using AutoMapper;
using MediatR;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Handlers.Question
{
    internal class QuestionRemoveHandler(IQuestionService service, IMapper mapper) : IRequestHandler<QuestionRemoveRequestDto>
    {
        public readonly IQuestionService service = service;
        public readonly IMapper mapper = mapper;
        public async Task Handle(QuestionRemoveRequestDto request, CancellationToken cancellationToken)
        {
            await service.Delete(request.Id);
        }
    }
}
