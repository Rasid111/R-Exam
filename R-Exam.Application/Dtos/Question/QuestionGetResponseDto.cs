using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.Question
{
    public class QuestionGetResponseDto : IRequest<QuestionGetResponseDto>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswerTitle { get; set; }
    }
}
