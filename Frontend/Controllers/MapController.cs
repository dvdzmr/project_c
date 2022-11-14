using Frontend.Models;
using Backend.DBconnection.CheckUserDB;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class MapController : Controller
{
    public List<Map> GetEvents()
    {
        var getdbevents = DBconnection.DBChecker();
        List<Map> test = new List<Map>();
        for (int i = Math.Max(0, getdbevents.Count - 5); i< getdbevents.Count; i++) // getdbevent.count - 5, 5 veranderen naar variable voor filter later
        {
            var dbevent = getdbevents[i];
            var allevent = new Map()
            {
                id = dbevent.id,
                time = dbevent.time,
                latitude = dbevent.latitude,
                longitude = dbevent.longitude,
                soundtype = dbevent.soundtype,
                probability = dbevent.probability,
                soundfile = dbevent.soundtype
            };
            test.Add(allevent);
        }
        test.Reverse();
        return test;
    }
    public PartialViewResult RecentEvents()
    {
        var test = GetEvents();
        return PartialView("RecentEvents", test);
    }
    // GET Map/EventList
    public async Task<IActionResult> Main()
    {
        var test = GetEvents();
        return PartialView(test);
    }
}