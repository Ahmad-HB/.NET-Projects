using Domain.Entity;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace Infrastructure.Data;

public class FreeBookDbContext : IdentityDbContext<ApplicationUser>
{
    public FreeBookDbContext(DbContextOptions<FreeBookDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        //builder.Entity<IdentityUser>().ToTable("Users", "Identity");
        //builder.Entity<IdentityRole>().ToTable("Roles", "Identity");
        //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
        //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
        //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
        //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Identity");
        //builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");

        builder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<LogCategory>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<SubCategory>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<LogSubCategory>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<LogBook>().Property(x => x.Id).HasDefaultValueSql("(UUID())");
        builder.Entity<VwUser>(Entity =>
        {
            Entity.HasNoKey();
            Entity.ToView("VwUsers");
        });

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<LogCategory> LogCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<LogSubCategory> LogSubCategories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<LogBook> LogBooks { get; set; }
    public DbSet<VwUser> VwUsers { get; set; }
    
}