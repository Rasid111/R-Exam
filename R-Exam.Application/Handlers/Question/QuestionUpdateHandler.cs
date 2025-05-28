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
    public class QuestionUpdateHandler(IQuestionService service) : IRequestHandler<QuestionUpdateRequestDto>
    {
        private readonly IQuestionService service = service;

        public async Task Handle(QuestionUpdateRequestDto request, CancellationToken cancellationToken)
        {
            await service.Update(request);
        }
    }
}
