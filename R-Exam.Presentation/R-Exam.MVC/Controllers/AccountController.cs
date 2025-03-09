using Microsoft.AspNetCore.Mvc;
using R_Exam.Application.Dtos.User;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace R_Exam.MVC.Controllers
{
    public class AccountController(HttpClient httpClient) : Controller
    {
        private readonly HttpClient httpClient = httpClient;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestDto user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("http://localhost:5266/api/account/register", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Login));
            return RedirectToAction(nameof(Register));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("http://localhost:5266/api/account/login", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return base.RedirectToAction(nameof(Login));
        }
    }
}
