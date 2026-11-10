using Microsoft.AspNetCore.Mvc;

namespace UnityFunds.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
