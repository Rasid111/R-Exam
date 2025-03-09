using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R_Exam.Infrastructre.EntityFramework;
using System.Data;
using R_Exam.Domain.Models;
using R_Exam.Infrastructre;
using R_Exam.Application.Dtos.User;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace R_Exam.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ApiController]
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext dbContext) : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly AccountDbContext dbContext = dbContext;

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Register(UserRegisterRequestDto newUser)
        {
            var user = new IdentityUser
            {
                UserName = newUser.Name,
                Email = newUser.Email,
            };
            var result = await this.userManager.CreateAsync(user, newUser.Password!);

            if (result.Succeeded)
            {
                await this.userManager.AddToRolesAsync(user, [nameof(Roles.User)]);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    Console.WriteLine(item.Description);
                    throw new ArgumentException("Invalid credentials", nameof(newUser));
                }
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto loginDto)
        {
            var foundUser = await this.userManager.FindByEmailAsync(loginDto.Email);
            if (foundUser == null)
                return Unauthorized();

            var signInResult = await signInManager.PasswordSignInAsync(foundUser, loginDto.Password, true, true);
            
            if (signInResult.Succeeded == false)
                return Unauthorized();
            return Ok();
        }
    }
}
