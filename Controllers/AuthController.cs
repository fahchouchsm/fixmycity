using fixmycity.dto.Response;
using fixmycity.models;
using fixmycity.security;
using Microsoft.AspNetCore.Mvc;

namespace fixmycity.Controllers;

public class AuthController(CurrentUser currentUser) : BaseApiController
{
    [HttpGet("me")]
    public IActionResult Me()
    {
        User user = new User();
        user.Id = currentUser.Id;
        user.Role = currentUser.Role;
        return Ok(new ApiResponseDTO<User>("hello", user));
    }
}