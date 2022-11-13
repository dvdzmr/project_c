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
        // var testing = new Map();
        // testing.mylist = new List<Test>()
        // {
        //     new Test()
        //     {
        //         id = 1,
        //         probability = 50,
        //         soundtype = "gun"
        //     },
        //     new Test()
        //     {
        //         id = 2,
        //         probability = 40,
        //         soundtype = "thunder"
        //     },
        //     new Test()
        //     {
        //         id = 3,
        //         probability = 24,
        //         soundtype = "unknown"
        //     },
        //     new Test()
        //     {
        //         id = 4,
        //         probability = 90,
        //         soundtype = "animal"
        //     },
        //     new Test()
        //     {
        //         id = 5,
        //         probability = 67,
        //         soundtype = "vehicle"
        //     },
        // };
        Console.WriteLine(test[0].ToString());
        return PartialView(test);
    }
}