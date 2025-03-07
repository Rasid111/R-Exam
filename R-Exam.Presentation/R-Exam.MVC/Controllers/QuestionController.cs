using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R_Exam.MVC.ViewModels;
using System.Net.Http;

namespace R_Exam.MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly HttpClient _httpClient;
        public QuestionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: QuestionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5266/api/Question/{id}");

            if (response.IsSuccessStatusCode)
            {
                var question = await response.Content.ReadFromJsonAsync<Question>();
                return View(question);
            }
            return View(null);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
