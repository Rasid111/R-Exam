using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using R_Exam.Application.Dtos.Question;
using R_Exam.Presentation.Models;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace R_Exam.Presentation.Controllers
{
    public class QuestionController(ISender sender, IMapper mapper) : Controller
    {
        private readonly ISender sender = sender;
        private readonly IMapper mapper = mapper;
        public async Task<IActionResult> Index()
        {
            if (!string.IsNullOrWhiteSpace((string?)TempData["ErrorMessage"]))
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                return View();
            }
            var response = await this.sender.Send(new QuestionGetAllRequestDto());
            var questions = mapper.Map<List<QuestionDetailsViewModel>>(response);
            return View(questions);
        }
        public async Task<ActionResult> Details(int id)
        {
            if (!string.IsNullOrWhiteSpace((string?)TempData["ErrorMessage"]))
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                return View();
            }
            var response = await this.sender.Send(new QuestionGetRequestDto(id: id));
            var questions = mapper.Map<QuestionDetailsViewModel>(response);
            return View(questions);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(QuestionCreateViewModel model)
        {
            if (!string.IsNullOrWhiteSpace((string?)TempData["ErrorMessage"]))
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                return View();
            }
            if (ModelState.IsValid)
            {
                var questionDto = mapper.Map<QuestionCreateRequestDto>(model);
                var response = await this.sender.Send(questionDto);
                TempData["SuccessMessage"] = $"Product was created with id {response.Id}";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }
    }
}
