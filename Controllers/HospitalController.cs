using hospital.Data;
using hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hospital.Controllers;

public class HospitalController : Controller
{
    private readonly ILogger<HospitalController> _logger;
    private readonly RowDbContext _dbcontext;

    public HospitalController(ILogger<HospitalController> logger, RowDbContext context)
    {
        _logger = logger;
        _dbcontext = context;
    }
    
    [HttpGet]
    public IActionResult TakeQueue()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TakeQueue([FromForm] RowViewModel model)
    {
        var user = new RowViewModel();
        user.Id = model.Id;
        user.Fullname = model.Fullname;
        user.CreatedAt = model.CreatedAt = DateTimeOffset.UtcNow.ToLocalTime();
        user.Phone = model.Phone;
        user.RowTime = model.CreatedAt.AddMinutes(45);

        try
        {
            _dbcontext.rows.Add(user);
            _dbcontext.SaveChanges();
        }

        catch(ArgumentNullException)
        {
            System.Console.WriteLine("Rejected");
        }
        return RedirectToAction("Show Row", user);
    }
    
    [HttpGet]
    public IActionResult ShowQueue([FromRoute]RowViewModel model)
    {
        var client =_dbcontext.rows.FirstOrDefault(u => u.Id == model.Id);
        return View(client);
    }
}