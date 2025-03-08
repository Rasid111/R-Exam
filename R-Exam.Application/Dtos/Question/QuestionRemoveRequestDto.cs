using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionRemoveRequestDto(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}
