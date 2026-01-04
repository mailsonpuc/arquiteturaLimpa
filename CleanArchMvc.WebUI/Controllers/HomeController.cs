using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.WebUI.Models;

namespace CleanArchMvc.WebUI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.UserName = User.Identity?.Name;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 50, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
