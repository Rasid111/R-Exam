using AutoMapper;
using Azure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Dtos.User;
using R_Exam.Application.Services;
using R_Exam.Domain.Enums;
using R_Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Handlers.User
{
    public class UserRegisterHandler(UserManager<IdentityUser> userManager) : IRequestHandler<UserRegisterRequestDto, UserRegisterResponseDto>
    {
        private readonly UserManager<IdentityUser> userManager = userManager;

        public async Task<UserRegisterResponseDto> Handle(UserRegisterRequestDto request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Name,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var createdUser = await userManager.FindByEmailAsync(user.Email);

                await userManager.AddToRolesAsync(createdUser!, [
                    // nameof(Roles.Admin),
                    nameof(Roles.User),
                ]);

                if (request.Avatar != null)
                {
                    var extension = new FileInfo(request.Avatar.FileName).Extension;
                    var directoryInfo = Directory.CreateDirectory("Avatars");
                    string avatarPath = Path.Combine(directoryInfo.FullName, $"{createdUser!.Id}_Avatar{extension}");
                    using var avatarFileStream = File.Create(avatarPath);
                    await userManager.AddClaimAsync(user, new Claim("AvatarPath", avatarPath));
                    await request.Avatar.CopyToAsync(avatarFileStream, cancellationToken);
                }
            }

            var response = new UserRegisterResponseDto()
            {
                Success = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description
            };

            return response;
        }
    }
}
