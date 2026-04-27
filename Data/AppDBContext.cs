using Microsoft.EntityFrameworkCore;
using fixmycity.model;

namespace fixmycity.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<CivilReport> CivilReports => Set<CivilReport>();
    public DbSet<ReportImage> ReportImages => Set<ReportImage>();
    public DbSet<ReportComment> ReportComments => Set<ReportComment>();
    public DbSet<ReportStatusHistory> ReportStatusHistories => Set<ReportStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReportComment>()
            .HasOne(rc => rc.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(rc => rc.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ReportComment>()
            .HasOne(rc => rc.CivilReport)
            .WithMany(r => r.Comments)
            .HasForeignKey(rc => rc.CivilReportId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CivilReport>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reports)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction); // 🔥 IMPORTANT
    }
}