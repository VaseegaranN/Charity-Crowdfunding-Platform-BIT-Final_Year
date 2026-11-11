
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UnityFunds.Web.Data.Factories;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=UnityFunds;Trusted_Connection=True;TrustServerCertificate=True");

        return new AppDbContext(options.Options);
    }
}
