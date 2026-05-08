using fixmycity.dto.Response;
using fixmycity.security;
using fixmycity.Services;
using Microsoft.AspNetCore.Mvc;

namespace fixmycity.Controllers;

public class AuthController(CurrentUser currentUser, IUserService userService) : BaseApiController
{
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        MeResDTO? user = await userService.GetMeAsync(currentUser.Id);

        if (user == null)
            return NotFound(ApiErrorResponse.Fail("User not found"));

        return Ok(ApiResponse<MeResDTO>.Ok(user));
    }
}