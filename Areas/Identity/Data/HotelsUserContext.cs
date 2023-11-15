using Hotels.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hotels.Areas.Identity.Data;


namespace Hotels.Data;

public class HotelsUserContext : IdentityDbContext<IdentityUser>
{
    public HotelsUserContext(DbContextOptions<HotelsUserContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}