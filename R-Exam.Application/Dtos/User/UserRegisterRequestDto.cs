using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using R_Exam.Application.Dtos.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.User
{
    public class UserRegisterRequestDto : IRequest<UserRegisterResponseDto>
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [MinLength(6, ErrorMessage = "Password must contain at least 6 symbols")]
        [Required]
        public required string Password { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
