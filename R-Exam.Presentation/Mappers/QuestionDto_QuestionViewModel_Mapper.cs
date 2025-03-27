using AutoMapper;
using R_Exam.Application.Dtos.Question;
using R_Exam.Presentation.Models;

namespace R_Exam.Presentation.Mappers
{
    public class QuestionDto_QuestionViewModel_Mapper : Profile
    {
        public QuestionDto_QuestionViewModel_Mapper()
        {
            CreateMap<QuestionCreateViewModel, QuestionCreateRequestDto>()
                .ForMember(dest => dest.Answers, config => config.MapFrom(src => src.Answers.Split(new string[] { "\r\n", "\r" }, StringSplitOptions.None)));
            CreateMap<QuestionGetResponseDto, QuestionDetailsViewModel>();
        }
    }
}
