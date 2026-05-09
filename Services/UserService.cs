using fixmycity.dto.Response;
using fixmycity.DTOs.Req;
using fixmycity.models;
using fixmycity.Repositories;
using fixmycity.security;

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

    public async Task RegisterUser(RegisterDto dto,CurrentUser currentUser)
    {
        User user = new User()
        {
            Id = currentUser.Id,
            FirstName = currentUser.name!,
            LastName = currentUser.lastName!,
            Email = currentUser.email!,
            Role = currentUser.Role,
        };

        await userRepository.RegisterUserAsync(user);
    }
}