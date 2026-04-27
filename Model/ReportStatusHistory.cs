using System.ComponentModel.DataAnnotations;
using fixmycity.Enum;
namespace fixmycity.model;
public class ReportStatusHistory
{
    [Key]
    public Guid Id { get; set; }
    public Guid CivilReportId { get; set; }
    public ReportStatus Status { get; set; }
    public string ChangedByUserId { get; set; } = null!;
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string? Comment { get; set; }

    public CivilReport CivilReport { get; set; } = null!;
    public User ChangedByUser { get; set; } = null!;
}