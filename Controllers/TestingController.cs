using Microsoft.AspNetCore.Mvc;

namespace fixmycity.Controllers;

[ApiController]
[Route("city/testing")]
public class TestingController : ControllerBase
{
    [HttpGet("test")]
    public ActionResult<string> Test()
    {
        return "hello world";
    }
}

