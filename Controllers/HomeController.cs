using System;
using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hospital.Data;
using hospital.Models;
using hospital.ViewModels;

namespace Queue.Controllers;

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
        var rows = _dbcontext.rows.OrderBy(p=>p.CreatedAt).ToList();
        return View(rows);
    }

    [HttpGet]
    public IActionResult Admin()
    {
        var rows = _dbcontext.rows.OrderBy(p => p.CreatedAt).ToList();
        return View(rows);
    }

    [HttpPost]
    public IActionResult Admin([FromForm]RowViewModel obj)
    {
        if (!_dbcontext.rows.Any(t => t.Id == obj.Id))
        {
            return NotFound();
        }
        _dbcontext.rows.Remove(obj);
        _dbcontext.SaveChanges();
        TempData["success"] = " Mijozga hizmat ko'rsatildi.!!";
        return RedirectToAction("Adminpage");
    }

    [HttpGet]
    public IActionResult TakeRow()
    {
        return View();
    }
    [HttpPost]
    public IActionResult TakeRow([FromForm]RowViewModel model)
    {
        var first = _dbcontext.rows.Any();

        if(!first)
        {
            var user = new RowViewModel();
            user.Id = model.Id;
            user.Fullname = model.Fullname;
            user.CreatedAt = model.CreatedAt = DateTimeOffset.UtcNow.ToLocalTime();
            user.Phone = model.Phone;
            user.IsActive = model.IsActive = true;
            try
            {
                _dbcontext.rows.Add(user);
                _dbcontext.SaveChanges();
            }
            catch(ArgumentNullException)
            {
                System.Console.WriteLine("Null Keldi");
            }
            return RedirectToAction("ShowRow",user);
        }

        else
        {
            var lastuser = _dbcontext.rows.OrderBy(p => p.CreatedAt).LastOrDefault();
            var user=new RowViewModel();
            user.Id = model.Id;
            user.Fullname = model.Fullname;
            user.CreatedAt = model.CreatedAt = DateTimeOffset.UtcNow.ToLocalTime();
            user.Phone=model.Phone;
            user.IsActive = model.IsActive = true;
            try
            {
                _dbcontext.rows.Add(user);
                _dbcontext.SaveChanges();
            }
            catch(ArgumentNullException)
            {
                System.Console.WriteLine("Null Keldi");
            }
            return RedirectToAction("ShowRow",user);
        }

    }
    
    [HttpGet]
    public IActionResult ShowRow([FromRoute]RowViewModel model)
    {
        var client =_dbcontext.rows.FirstOrDefault(u => u.Id == model.Id);
        return View(client);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}