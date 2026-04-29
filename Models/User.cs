using System.ComponentModel.DataAnnotations;
namespace fixmycity.models;
public class User
{
    [Key]
    [MaxLength(64)]
    public string Id { get; set; } = null!;

    [MaxLength(20)]
    public string Role { get; set; } = null!;

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CivilReport> Reports { get; set; } = new List<CivilReport>();
    public ICollection<ReportComment> Comments { get; set; } = new List<ReportComment>();
}