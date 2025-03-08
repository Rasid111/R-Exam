using AutoMapper;
using R_Exam.Application.Dtos.Question;
using R_Exam.MVC.Models;

namespace R_Exam.Application.Mappers
{
    public class Question_QuestionViewModel_Mapper : Profile
    {
        public Question_QuestionViewModel_Mapper()
        {
            CreateMap<QuestionCreateViewModel, QuestionCreateRequestDto>()
                .ForMember(dest => dest.Answers, config => config.MapFrom(src => src.Answers.Split(new string[] { "\r\n", "\r" }, StringSplitOptions.None)));
        }
    }
}
