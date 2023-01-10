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
    public List<MapItems> GetEvents(int evenAmounts, string value1 = "All", string value2 = "1")
    {
        var getEvents = DBquery.DBquery.DbChecker();
        List<MapItems> events = new List<MapItems>();
        int val2 = int.Parse(value2);
        
        var dict = new Dictionary<int, Tuple<string, string>>()
        {
            { 1, new Tuple<string, string>("00:00", "23:00") },
            { 2, new Tuple<string, string>("00:00", "01:00") },
            { 3, new Tuple<string, string>("01:00", "02:00") },
            { 4, new Tuple<string, string>("02:00", "03:00") },
            { 5, new Tuple<string, string>("03:00", "04:00") },
            { 6, new Tuple<string, string>("04:00", "05:00") },
            { 7, new Tuple<string, string>("05:00", "06:00") },
            { 8, new Tuple<string, string>("06:00", "07:00") },
            { 9, new Tuple<string, string>("07:00", "08:00") },
            { 10, new Tuple<string, string>("08:00", "09:00") },
            { 11, new Tuple<string, string>("09:00", "10:00") },
            { 12, new Tuple<string, string>("10:00", "11:00") },
            { 13, new Tuple<string, string>("11:00", "12:00") },
            { 14, new Tuple<string, string>("12:00", "13:00") },
            { 15, new Tuple<string, string>("13:00", "14:00") },
            { 16, new Tuple<string, string>("14:00", "15:00") },
            { 17, new Tuple<string, string>("15:00", "16:00") },
            { 18, new Tuple<string, string>("16:00", "17:00") },
            { 19, new Tuple<string, string>("17:00", "18:00") },
            { 20, new Tuple<string, string>("18:00", "19:00") },
            { 21, new Tuple<string, string>("19:00", "20:00") },
            { 22, new Tuple<string, string>("20:00", "21:00") },
            { 23, new Tuple<string, string>("21:00", "22:00") },
            { 24, new Tuple<string, string>("22:00", "23:00") },
            { 25, new Tuple<string, string>("23:00", "24:00") },
        };
        
        if (getEvents.Count() < evenAmounts)
        {
            evenAmounts = getEvents.Count();
        }
        int j = 0;
        for (int i = 0; i < getEvents.Count && j < evenAmounts; i++)
            {
                var dbevent = getEvents[i];
                var dateTime1 = DateTime.ParseExact(dict[val2].Item1, "H:mm", null,
                    System.Globalization.DateTimeStyles.None);
                var dateTime2 = DateTime.ParseExact(dict[val2].Item2, "H:mm", null,
                    System.Globalization.DateTimeStyles.None);
                if ((value1 == "All" || value1 == dbevent.Soundtype) && (value2 == "1" || (dateTime1.TimeOfDay <= dbevent.Time.TimeOfDay && dateTime2.TimeOfDay > dbevent.Time.TimeOfDay)))
                {
                    var allevent = new MapItems()
                    {
                        Id = dbevent.Id,
                        Time = dbevent.Time,
                        Latitude = dbevent.Latitude,
                        Longitude = dbevent.Longitude,
                        Soundtype = dbevent.Soundtype,
                        Probability = dbevent.Probability,
                        Soundfile = dbevent.Soundfile,
                        Status = dbevent.Status
                    };
                    events.Add(allevent);
                    j++;
                }
            }
        
        // Console.WriteLine("Lat: {0} Long: {1}", test[0].Latitude, test[0].Longitude );
        return events;
    }

    public async Task<ActionResult> GetData (int addevent, string value1 = "All", string value2 = "1")
    {
        List<MapItems> getmapdata = GetEvents(addevent, value1, value2);
        return Json(getmapdata);
    }

    public async Task<ActionResult> GiveNotification()
    {
        string value1 = "All";
        string value2 = "1";
        // Notifications must invoked with conditions, in this case we can implement if conditions here by using GetEvents()
        // and having a settings file or something that stores what the filters are to implement here.
        //ex:
        List<MapItems> lastEvent = GetEvents(1, value1, value2); // only fetches the last event
        
        foreach (var Item in lastEvent)
        {
            if (Item.Soundtype == "Gunshot") // Condition
            {
                _notyf.Custom("Gunshot was heard", 5, "whitesmoke", "fa fa-gear");
                if (Item.Probability > 30) // If probability is higher than 50%; these can be turned into variables.
                {
                    _notyf.Custom($"Gunshot was heard with {Item.Probability} probability", 5, "whitesmoke", "fa fa-gear");
                }
            }
            if (Item.Soundtype == "Animal") // Condition
            {
                _notyf.Custom("Animal was heard", 5, "whitesmoke", "fa fa-gear");
            }
            if (Item.Soundtype == "Vehicle") // Condition
            {
                _notyf.Custom("A Vehicle was heard", 5, "whitesmoke", "fa fa-gear");
            }
            if (Item.Soundtype == "Thunder") // Condition
            {
                _notyf.Custom("Thunder was heard", 5, "whitesmoke", "fa fa-gear");
            }
        }
        return Json(1);
    }
    public PartialViewResult Events(int addevent = 0, string value1 = "all", string value2 = "1")
    {
        // Maybe cache the last time GetEvents was ran and compare what is different to decide what to put into
        // the foreach loop
        List<MapItems> fiveEvents = GetEvents(5+addevent, value1, value2); //required data for updating with interval after initial loading 
        return PartialView(fiveEvents);
    }
    
    public IActionResult Main()
    {
        var test = GetEvents(5); // data for recent events (view Map)
        return View(test);
    }
    public ActionResult ViewDetailsPartial(int eventid = 1)
    {
        eventid -= 1;
        var getEvents = DBquery.DBquery.DbChecker();
        getEvents.Reverse();
        MapItems tmp = new MapItems()
        {
            Id = getEvents[eventid].Id,
            Time = getEvents[eventid].Time,
            Latitude = getEvents[eventid].Latitude,
            Longitude = getEvents[eventid].Longitude,
            Soundtype = getEvents[eventid].Soundtype,
            Probability = getEvents[eventid].Probability,
            Soundfile = getEvents[eventid].Soundfile,
            Status = getEvents[eventid].Status
        };
        return PartialView(tmp);
    }
    public async Task<ActionResult> PushStatus (int mainkey, string status)
    {
        DBquery.DBquery.DbStatusPush(mainkey, status);
        return Json(1);
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