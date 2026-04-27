using Microsoft.AspNetCore.Mvc;

namespace fixmycity.Controllers;

[ApiController]
public class TestingController : BaseApiController
{
    [HttpGet("test")]
    public ActionResult<string> Test()
    {
        var userId = Request.Headers["X-User-Id"];
        var role = Request.Headers["X-User-Role"];

        return $"hello world | user={userId} | role={role}";
    }
}