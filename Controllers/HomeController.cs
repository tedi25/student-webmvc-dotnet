using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Models;

namespace WebAppStudent.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;

    private readonly IStudentService _studentService;

    public HomeController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllAsync(null);
        return View(students);
    }

    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentService.GetByIdWithCoursesAsync(id);

        if (student == null)
            return NotFound();

        return View(student);
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
