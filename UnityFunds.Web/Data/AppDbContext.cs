using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Models;

namespace UnityFunds.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AdminDetails> AdminDetails => Set<AdminDetails>();
    public DbSet<DonorDetails> DonorDetails => Set<DonorDetails>();
    public DbSet<NgoDetails> NgoDetails => Set<NgoDetails>();
    public DbSet<LogisticsDetails> LogisticsDetails => Set<LogisticsDetails>();
    public DbSet<GovernmentStaffDetails> GovernmentStaffDetails => Set<GovernmentStaffDetails>();
    public DbSet<PublicPost> PublicPosts => Set<PublicPost>();
    public DbSet<NgoPostDetail> NgoPostDetails => Set<NgoPostDetail>();
    public DbSet<AdminPost> AdminPosts => Set<AdminPost>();
    public DbSet<GovernmentPost> GovernmentPosts => Set<GovernmentPost>();
    public DbSet<PublicContactHelp> PublicContactHelp => Set<PublicContactHelp>();
    public DbSet<UserReport> UserReports => Set<UserReport>();
    public DbSet<GobiUser> GobiUsers => Set<GobiUser>();
    public DbSet<SathyUser> SathyUsers => Set<SathyUser>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<Donation> Donations => Set<Donation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AdminDetails>().Property(a => a.admin_id).UseIdentityColumn();
        modelBuilder.Entity<DonorDetails>().Property(d => d.donor_id).UseIdentityColumn();
        modelBuilder.Entity<PublicContactHelp>().Property(p => p.pch_id).UseIdentityColumn();
        modelBuilder.Entity<UserReport>().Property(u => u.id).UseIdentityColumn();
        modelBuilder.Entity<GobiUser>().Property(u => u.gobi_user_id).UseIdentityColumn();
        modelBuilder.Entity<SathyUser>().Property(u => u.sathy_user_id).UseIdentityColumn();
        modelBuilder.Entity<Campaign>().HasIndex(c => new { c.Status, c.CreatedAtUtc });
        modelBuilder.Entity<Campaign>().HasIndex(c => c.NgoId);
        modelBuilder.Entity<Donation>().HasIndex(d => new { d.CampaignId, d.Status });
        modelBuilder.Entity<Donation>().HasIndex(d => new { d.DonorId, d.CreatedAtUtc });
        modelBuilder.Entity<Donation>().HasOne<Campaign>().WithMany().HasForeignKey(d => d.CampaignId).OnDelete(DeleteBehavior.Restrict);
    }
}
