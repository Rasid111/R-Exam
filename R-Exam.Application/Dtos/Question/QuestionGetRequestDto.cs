using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionGetRequestDto(int id = 0, string? title = null) : IRequest<QuestionGetResponseDto>
    {
        public int Id { get; set; } = id;
        public string? Title { get; set; } = title;
    }
}
