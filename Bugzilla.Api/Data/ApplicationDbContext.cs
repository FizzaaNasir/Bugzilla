using Bugzilla.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bugzilla.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>()
              .HasOne(b => b.User)
              .WithMany(ba => ba.Project_User)
              .HasForeignKey(bi => bi.UserId);

            modelBuilder.Entity<ProjectUser>()
              .HasOne(b => b.Project)
              .WithMany(ba => ba.Project_User)
              .HasForeignKey(bi => bi.ProjectId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Bug> Bug { get; set; }
        public DbSet<ProjectUser> Project_User { get; set; }

    }
}

