using fixmycity.dto.Response;

namespace fixmycity.Services;

public interface IUserService
{
    public Task<MeResDTO?> GetMeAsync(string userId);
}