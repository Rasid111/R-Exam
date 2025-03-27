using MediatR;
using Microsoft.AspNetCore.Identity;
using R_Exam.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Handlers.User
{
    internal class UserLogoutHandler(SignInManager<IdentityUser> signInManager) : IRequestHandler<UserLogoutRequestDto, UserLogoutResponseDto>
    {
        SignInManager<IdentityUser> signInManager = signInManager;

        public async Task<UserLogoutResponseDto> Handle(UserLogoutRequestDto request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
            return new UserLogoutResponseDto();
        }
    }
}
