using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Exceptions;
using R_Exam.Presentation.Models;
using System.Reflection.Metadata.Ecma335;

namespace R_Exam.Presentation.Controllers
{
    [Route("[controller]")]
    public class QuestionController(ISender sender, IMapper mapper) : Controller
    {
        private readonly ISender sender = sender;
        private readonly IMapper mapper = mapper;
        public async Task<IActionResult> Index()
        {
            var response = await this.sender.Send(new QuestionGetAllRequestDto());
            var questions = mapper.Map<List<QuestionDetailsViewModel>>(response);
            return View(questions);
        }
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var response = await this.sender.Send(new QuestionGetRequestDto(id: id));
            var questions = mapper.Map<QuestionDetailsViewModel>(response);
            return View(questions);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id?}")]
        public ActionResult Update(int? id = null)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Question/Update")]
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
        [Authorize(Roles = "Admin")]
        [HttpGet("[Action]")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[Action]")]
        public async Task<ActionResult> Create(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionDto = mapper.Map<QuestionCreateRequestDto>(model);
                var response = await this.sender.Send(questionDto);
                TempData["SuccessMessage"] = $"Question was created with id {response.Id}";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteQuestion(id);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await sender.Send(new QuestionDeleteRequestDto(id));
            TempData["SuccessMessage"] = $"Question with id {id} was deleted";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Test()
        {
            var response = await this.sender.Send(new QuestionGetAllRequestDto());
            var questions = mapper.Map<List<QuestionDetailsViewModel>>(response);
            var model = new TestViewModel(questions) { QuestionsCount = questions.Count, Number = 0 };
            return View(model);
        }
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Test(TestViewModel testModel)
        {
            if (testModel.SelectedAnswer is not null && testModel.Questions[testModel.Number].CorrectAnswerTitle == testModel.SelectedAnswer)
            {
                TempData["SuccessMessage"] = $"Your previous answer was right";
            }
            else
            {
                TempData["ErrorMessage"] = $"Your previous answer was wrong";
            }
            if (testModel.Number < testModel.QuestionsCount - 1)
            {
                testModel.Number++;
                return View(testModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
