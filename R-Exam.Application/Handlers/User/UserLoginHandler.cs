using MediatR;
using Microsoft.AspNetCore.Identity;
using R_Exam.Application.Dtos.User;
using R_Exam.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Handlers.User
{
    public class UserLoginHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : IRequestHandler<UserLoginRequestDto, UserLoginResponseDto>
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;

        public async Task<UserLoginResponseDto> Handle(UserLoginRequestDto request, CancellationToken cancellationToken)
        {
            var foundUser = await this.userManager.FindByEmailAsync(request.Email);

            if (foundUser == null) {
                return new UserLoginResponseDto()
                {
                    Success = false,
                    Error = "Incorrect email or password"
                };
            }

            var result = await signInManager.PasswordSignInAsync(foundUser, request.Password, true, false);

            if (!result.Succeeded)
            {
                return new UserLoginResponseDto()
                {
                    Success = false,
                    Error = "Incorrect email or password"
                };
            }
            return new UserLoginResponseDto() { Success = true };
        }
    }
}
