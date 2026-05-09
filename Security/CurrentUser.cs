namespace fixmycity.security;

public class CurrentUser
{
    public string Id { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? email { get; set; } 
    public string? name { get; set; }
    public string? lastName { get; set; }
}