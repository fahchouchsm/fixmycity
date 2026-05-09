using fixmycity.dto.Response;
using fixmycity.DTOs.Req;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto) 
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiErrorResponse.Fail("Invalid body"));

        if (string.IsNullOrEmpty(currentUser.email) ||
            string.IsNullOrEmpty(currentUser.name) ||
            string.IsNullOrEmpty(currentUser.lastName))
        {
            return BadRequest();
        }

        await userService.RegisterUser(dto, currentUser);
        return Ok(currentUser);
    }
}