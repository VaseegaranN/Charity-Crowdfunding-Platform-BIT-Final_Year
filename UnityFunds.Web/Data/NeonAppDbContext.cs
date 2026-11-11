
using Microsoft.EntityFrameworkCore;

namespace UnityFunds.Web.Data;

public class NeonAppDbContext : AppDbContext
{
    public NeonAppDbContext(
        DbContextOptions<NeonAppDbContext> options)
        : base(options)
    {
    }
}
