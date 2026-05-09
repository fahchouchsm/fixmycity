namespace fixmycity.Enums;

public static class GatewayHeader
{
    private const string Prefix = "X-User-";
    public const string Id = Prefix + "Id";
    public const string Role = Prefix + "Role";
    public const string Email = Prefix + "Email";
    public const string Name = Prefix + "Name";
    public const string LastName = Prefix + "LastName";
    public const string IsEmailVerified = Prefix + "IsEmailVerified";
}