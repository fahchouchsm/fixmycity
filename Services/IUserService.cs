using fixmycity.dto.Response;
using fixmycity.DTOs.Req;
using fixmycity.security;

namespace fixmycity.Services;

public interface IUserService
{
    public Task<MeResDTO?> GetMeAsync(string userId);
    public Task RegisterUser(RegisterDto dto, CurrentUser currentUser);
}