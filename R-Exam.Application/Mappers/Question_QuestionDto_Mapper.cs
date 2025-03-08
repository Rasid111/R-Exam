using AutoMapper;
using R_Exam.Application.Dtos.Question;
using R_Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Mappers
{
    public class Question_QuestionDto_Mapper : Profile
    {
        public Question_QuestionDto_Mapper()
        {
            CreateMap<QuestionCreateRequestDto, Question>()
                .ForMember(dest => dest.Answers, config => config.MapFrom(src => src.Answers.Select(answer => new Answer { Title = answer }).ToList()));
            CreateMap<Question, QuestionGetResponseDto>()
                .ForMember(dest => dest.Answers, config => config.MapFrom(src => src.Answers.Select(answer => answer.Title).ToList()));
            CreateMap<QuestionUpdateRequestDto, Question>()
                .ForMember(dest => dest.Answers, config => config.MapFrom(src => src.Answers.Select(answer => new Answer { Title = answer }).ToList()));
        }
    }
}
