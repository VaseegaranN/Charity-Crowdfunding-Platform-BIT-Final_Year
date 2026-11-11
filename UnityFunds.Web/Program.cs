using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Services;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(
        new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute()
    ));

builder.Services.AddScoped<PasswordService>();

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("auth", o =>
    {
        o.PermitLimit = 8;
        o.Window = TimeSpan.FromMinutes(1);
        o.QueueLimit = 0;
    });
});

// Session cache
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".UnityFunds.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Database
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(
//         builder.Configuration.GetConnectionString("DefaultConnection")
//     ));


// var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (builder.Environment.IsProduction())
{

    var databaseUrl = builder.Configuration["DATABASE_URL"];

    if (string.IsNullOrWhiteSpace(databaseUrl))
    {
        throw new InvalidOperationException(
            "DATABASE_URL is missing from production configuration.");
    }

    if (!Uri.TryCreate(databaseUrl, UriKind.Absolute, out var databaseUri))
    {
        throw new InvalidOperationException(
            "DATABASE_URL is not a valid PostgreSQL URL.");
    }

    var userInfo = databaseUri.UserInfo.Split(':', 2);

    if (userInfo.Length != 2 ||
        string.IsNullOrWhiteSpace(userInfo[0]) ||
        string.IsNullOrWhiteSpace(userInfo[1]))
    {
        throw new InvalidOperationException(
            "DATABASE_URL has invalid credentials.");
    }

    var connectionString =
        new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
            Database = Uri.UnescapeDataString(
                databaseUri.AbsolutePath.TrimStart('/')),
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = Uri.UnescapeDataString(userInfo[1]),
            SslMode = Npgsql.SslMode.Require,
            Pooling = true
        }.ConnectionString;

    builder.Services.AddDbContext<NeonAppDbContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddScoped<AppDbContext>(sp =>
        sp.GetRequiredService<NeonAppDbContext>());

}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));
}


var app = builder.Build();

// Do not run Database.Migrate() automatically here.
// Apply production database migrations separately before deployment.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Vercel handles HTTPS at its edge.
// Avoid HTTPS redirection loops inside the container.
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] =
        "strict-origin-when-cross-origin";
    context.Response.Headers["Permissions-Policy"] =
        "camera=(), microphone=(), geolocation=()";

    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseRateLimiter();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "campaigns",
    pattern: "campaigns/{action=Index}/{id?}",
    defaults: new { controller = "Campaigns" });

app.Run();
