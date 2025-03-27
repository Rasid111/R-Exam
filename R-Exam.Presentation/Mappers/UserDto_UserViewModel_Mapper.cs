using AutoMapper;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Dtos.User;
using R_Exam.Presentation.Models;

namespace R_Exam.Presentation.Mappers
{
    public class UserDto_UserViewModel_Mapper : Profile
    {
        public UserDto_UserViewModel_Mapper()
        {
            CreateMap<UserLoginResponseDto, UserViewModel>();
            CreateMap<UserViewModel, UserLoginResponseDto > ();
        }
    }
}
