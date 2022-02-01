using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hospital.Models;
using hospital.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using hospital.Data;

namespace hospital.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RowDbContext _dbcontext;

    public HomeController(ILogger<HomeController> logger, RowDbContext context)
    {
        _logger = logger;
        _dbcontext = context;
    }

    
    public IActionResult Index()
    {
        var queues = _dbcontext.rows.ToList();
        return View(queues);
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