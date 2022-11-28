using System.Diagnostics;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INotyfService _notyf;
    private List<MapItems> map;
    public HomeController(ILogger<HomeController> logger, INotyfService notyf)
    {
        _logger = logger;
        _notyf = notyf;
        map = new List<MapItems>();
    }
    
    
    
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated) //Redirect to main if user is logged in.
        {
            
            return LocalRedirect("/Home/Main");
            
        }
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
    public List<MapItems> GetEvents(int evenAmounts = 5)
    {
        var getEvents = DBquery.DBquery.DbChecker();
        List<MapItems> test = new List<MapItems>();
        for (int i = Math.Max(0, getEvents.Count - evenAmounts); i < getEvents.Count; i++)
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
        // Maybe cache the last time GetEvents was ran and compare what is different to decide what to put into
        // the foreach loop

        // Notifications must invoked with conditions, in this case we can implement if conditions here by using GetEvents()
        // and having a settings file or something that stores what the filters are to implement here.
        //ex:
        List<MapItems> lastEvent = GetEvents(1); // only fetches the last event
        
        foreach (var Item in lastEvent)
        {
            if (Item.Soundtype == "gunshot") // Condition
            {
                _notyf.Custom("Gunshot was heard", 5, "whitesmoke", "fa fa-gear");
                if (Item.Probability > 50) // If probability is higher than 50%; these can be turned into variables.
                {
                    _notyf.Custom("Gunshot was heard with 50% + probability", 5, "whitesmoke", "fa fa-gear");
                }
            }
            if (Item.Soundtype == "animal") // Condition
            {
                _notyf.Custom("Animal was heard", 5, "whitesmoke", "fa fa-gear");
            }
            if (Item.Soundtype == "vehicle") // Condition
            {
                _notyf.Custom("A Vehicle was heard", 5, "whitesmoke", "fa fa-gear");
            }
            if (Item.Soundtype == "thunder") // Condition
            {
                _notyf.Custom("Thunder was heard", 5, "whitesmoke", "fa fa-gear");
            }
        }
        List<MapItems> fiveEvents = GetEvents(5); //required data for updating with interval after initial loading 
        return PartialView(fiveEvents);
    }
    
    public IActionResult Main()
    {
        var test = GetEvents(); // data for recent events (view Map)
        return View(test);
    }
    public ActionResult ViewDetailsPartial(int test = 0)
    {
        var getEvents = DBquery.DBquery.DbChecker();
        MapItems tmp = new MapItems()
        {
            Id = getEvents[test].Id,
            Time = getEvents[test].Time,
            Latitude = getEvents[test].Latitude,
            Longitude = getEvents[test].Longitude,
            Soundtype = getEvents[test].Soundtype,
            Probability = getEvents[test].Probability,
            Soundfile = getEvents[test].Soundtype
        };
        return PartialView(tmp);
    }

    // public JsonResult ViewDetailsPost(MapItems test)
    // {
    //     MapItems mapItems = new MapItems
    //     {
    //         Id = test.Id,
    //         Latitude = test.Latitude,
    //         Longitude = test.Longitude,
    //         Probability = test.Probability,
    //         Soundfile = test.Soundfile,
    //         Soundtype = test.Soundtype
    //     };
    //     return Json(mapItems);
    // }
}