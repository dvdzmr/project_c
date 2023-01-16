using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;

namespace Frontend.Controllers;

public class ErrorNoAccesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private List<MapItems> map;
    
    public ErrorNoAccesController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult NoAcces()
    {
        return View();
    }
}