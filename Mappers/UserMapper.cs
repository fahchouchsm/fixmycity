using fixmycity.dto.Response;
using fixmycity.models;

namespace fixmycity.Mappers;

public class UserMapper
{
    public static MeResDTO ToMeResDTO(User user)
    {
        return new MeResDTO
        {
            Id = user.Id,
            Role = user.Role,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}