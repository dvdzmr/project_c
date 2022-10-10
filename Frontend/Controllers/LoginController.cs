using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
    
  /*  public IActionResult CheckUserDB()
    {
        return View("succesloginpage");
    }*/ //Ik had het eerst met dit geprobeerd, doordat ik ergins online las dat je het hier moest aangeven samen met die argumenten. Maar dat -
  //werkte niet.
  public IActionResult succesloginpage()
  {
      return View(); //Hier brengt het je dus naar de "succesloginpage" dat gwn de scherm is nadat je inglogd
  }
}