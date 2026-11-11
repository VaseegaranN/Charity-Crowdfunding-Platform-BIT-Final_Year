
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using UnityFunds.Web.Data;

namespace UnityFunds.Web.Data.Factories;

public class NeonAppDbContextFactory
    : IDesignTimeDbContextFactory<NeonAppDbContext>
{
    public NeonAppDbContext CreateDbContext(string[] args)
    {
        var databaseUrl =
            Environment.GetEnvironmentVariable("DATABASE_URL");

        if (string.IsNullOrWhiteSpace(databaseUrl))
            throw new InvalidOperationException(
                "Set DATABASE_URL before generating Neon migrations.");

        var uri = new Uri(databaseUrl);
        var credentials = uri.UserInfo.Split(':', 2);

        if (credentials.Length != 2)
            throw new InvalidOperationException(
                "Invalid PostgreSQL connection URL.");

        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = Uri.UnescapeDataString(
                uri.AbsolutePath.TrimStart('/')),
            Username = Uri.UnescapeDataString(credentials[0]),
            Password = Uri.UnescapeDataString(credentials[1]),
            SslMode = SslMode.Require
        }.ConnectionString;

        var options =
            new DbContextOptionsBuilder<NeonAppDbContext>();

        options.UseNpgsql(connectionString);

        return new NeonAppDbContext(options.Options);
    }
}
