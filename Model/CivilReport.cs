using System.ComponentModel.DataAnnotations;
using fixmycity.Enum;
namespace fixmycity.model;
public class CivilReport
{
    [Key]
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ReportStatus Status { get; set; } = ReportStatus.IN_PROGRESS;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? AddressText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public ICollection<ReportImage> Images { get; set; } = new List<ReportImage>();
    public ICollection<ReportComment> Comments { get; set; } = new List<ReportComment>();
    public ICollection<ReportStatusHistory> StatusHistory { get; set; } = new List<ReportStatusHistory>();
}