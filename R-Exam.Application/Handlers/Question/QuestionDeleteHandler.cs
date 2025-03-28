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
    internal class QuestionDeleteHandler(IQuestionService service) : IRequestHandler<QuestionDeleteRequestDto>
    {
        public readonly IQuestionService service = service;
        public async Task Handle(QuestionDeleteRequestDto request, CancellationToken cancellationToken)
        {
            await service.Delete(request);
        }
    }
}
