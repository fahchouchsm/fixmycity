using System.ComponentModel.DataAnnotations;
using fixmycity.enums;
namespace fixmycity.models;
public class ReportImage
{
    [Key]
    public Guid Id { get; set; }
    public Guid CivilReportId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public MediaType MediaType { get; set; } = MediaType.IMAGE;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public CivilReport CivilReport { get; set; } = null!;
}