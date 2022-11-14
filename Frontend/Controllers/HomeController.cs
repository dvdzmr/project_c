using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;

    
namespace Frontend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
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
    public List<MapItems> GetEvents()
    {
        var getEvents = DBquery.DBquery.DbChecker();
        List<MapItems> test = new List<MapItems>();
        for (int i = Math.Max(0, getEvents.Count - 5); i < getEvents.Count; i++)
        {
            var dbevent = getEvents[i];
            var allevent = new MapItems()
            {
                Id = dbevent.Id,
                Time = dbevent.Time,
                Latitude = dbevent.Latitude,
                Longitude = dbevent.Longitude,
                Soundtype = dbevent.Soundtype,
                Probability = dbevent.Probability,
                Soundfile = dbevent.Soundtype
            };
            test.Add(allevent);
        }

        test.Reverse();
        return test;
    }

    public PartialViewResult Map()
    {
        var test = GetEvents();
        //return PartialView("RecentEvents", test);
        return PartialView(test);
    }
    
    public async Task<IActionResult> Main()
    {
        var test = GetEvents();
        return PartialView(test);
    }
}