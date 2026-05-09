using System.ComponentModel.DataAnnotations;
namespace fixmycity.models;
public class User
{
    [Key]
    [MaxLength(64)]
    public string Id { get; init; } = null!;

    [MaxLength(20)]
    public string Role { get; init; } = null!;

    [MaxLength(100)]
    public required string FirstName { get; init; }

    [MaxLength(100)]
    public required string LastName { get; init; }
    [MaxLength(255)]
    public required string Email { get; init; }

    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public ICollection<CivilReport> Reports { get; init; } = new List<CivilReport>();
    public ICollection<ReportComment> Comments { get; init; } = new List<ReportComment>();
}