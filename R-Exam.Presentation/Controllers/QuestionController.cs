using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using R_Exam.Application.Dtos.Question;
using R_Exam.Presentation.Models;

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
        public ActionResult Update()
        {
            if (!string.IsNullOrWhiteSpace((string?)TempData["ErrorMessage"]))
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(QuestionUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionDto = mapper.Map<QuestionUpdateRequestDto>(model);
                await this.sender.Send(questionDto);
                TempData["SuccessMessage"] = $"Product with id {model.Id} was updated";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }
    }
}
