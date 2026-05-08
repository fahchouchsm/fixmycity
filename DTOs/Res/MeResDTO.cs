namespace fixmycity.dto.Response;

public class MeResDTO
{
    public string Id { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}