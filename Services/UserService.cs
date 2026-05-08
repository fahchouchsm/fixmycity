using fixmycity.dto.Response;
using fixmycity.models;
using fixmycity.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace fixmycity.Services;

public class UserService(UserRepository userRepository) : IUserService
{
    public async Task<MeResDTO?> GetMeAsync(string userId)
    {
        MeResDTO? user = await userRepository.GetByIdAsync(
            userId,
            u => new MeResDTO
            {
                Id = u.Id,
                Role = u.Role,
                FirstName = u.FirstName,
                LastName = u.LastName
            }
        );
        
        return user;
    }
}