using System.ComponentModel.DataAnnotations;
namespace fixmycity.models;
public class ReportComment
{
    [Key]
    public Guid Id { get; set; }
    public Guid CivilReportId { get; set; }
    public string UserId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public CivilReport CivilReport { get; set; } = null!;
    public User User { get; set; } = null!;
}