using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using R_Exam.Application.Dtos.Question;
using R_Exam.MVC.Models;
using System.Text;
using System.Text.Json;

namespace R_Exam.MVC.Controllers
{
    public class QuestionController(HttpClient httpClient, IMapper mapper) : Controller
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;

        // GET: QuestionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync($"http://localhost:5266/api/Question/{id}");

            if (response.IsSuccessStatusCode)
            {
                var question = await response.Content.ReadFromJsonAsync<QuestionDetailsViewModel>();
                return View(question);
            }
            return View(null);
        }

        // GET: QuestionController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = mapper.Map<QuestionCreateRequestDto>(model);
                var response = await httpClient.PostAsync("http://localhost:5266/api/Question/", new StringContent(JsonSerializer.Serialize(question), Encoding.UTF8, "application/json"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
