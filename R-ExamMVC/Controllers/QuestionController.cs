using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using R_ExamMVC.Models;

namespace R_ExamMVC.Controllers;

public class QuestionController : Controller
{
    private readonly ILogger<QuestionController> _logger;

    public QuestionController(ILogger<QuestionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
