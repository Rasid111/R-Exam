using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Dtos.User;
using R_Exam.Presentation.Models;
using System.Data;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace R_Exam.Presentation.Controllers
{
    public class AccountController(IMapper mapper, ISender sender) : Controller
    {
        private readonly IMapper mapper = mapper;
        private readonly ISender sender = sender;

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var user = new UserViewModel()
            {
                Email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value!,
                Name = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!,
                AvatarPath = User.Claims.FirstOrDefault(claim => claim.Type == "AvatarPath")?.Value,
                Roles = User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList(),
            };
            return View(user);
        }
        [HttpGet]
        public IActionResult Avatar(string avatarPath)
        {
            if (!System.IO.File.Exists(avatarPath))
            {
                return base.NotFound();
            }

            var stream = System.IO.File.Open(avatarPath, FileMode.Open);

            return base.File(stream, contentType: "image/jpeg");
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequestDto newUser)
        {
            var response = await sender.Send(newUser);
            if (!response.Success)
            {
                ModelState.AddModelError("All", response.Error!);
                return View();
            }
            return base.RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrWhiteSpace(returnUrl) == false)
            {
                ViewData["returnUrl"] = returnUrl;
            }
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto user)
        {
            var response = await sender.Send(user);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.Error;
                return base.RedirectToAction(actionName: nameof(Login));
            }

            if (!string.IsNullOrWhiteSpace(user.ReturnUrl))
            {
                return base.Redirect(user.ReturnUrl);
            }

            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await sender.Send(new UserLogoutRequestDto());
            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "Forbidden";
            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
