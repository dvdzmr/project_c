using Frontend.Models;
using Backend.DBconnection.CheckUserDB;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class MapController : Controller
{
    // GET Map/EventList
    public async Task<IActionResult> Main()
    {
        // Krijg list van database items.
        // var allevent = new Map();
        var getdbevents =  DBconnection.DbChecker();
        List<Map> test = new List<Map>();
        for (int i = Math.Max(0, getdbevents.Count - 5); i< getdbevents.Count; i++) // getdbevent.count - 5, 5 veranderen naar variable voor filter later
        {
            var dbevent = getdbevents[i];
            var allevent = new Map()
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
        Console.WriteLine(test[0].ToString());
        return PartialView(test);
    }
}