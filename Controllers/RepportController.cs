using Microsoft.AspNetCore.Mvc;

namespace fixmycity.Controllers;

public class RepportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}