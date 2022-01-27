using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public partial class IdContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public IdContext(DbContextOptions<IdContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers")
                .HasKey(r => new
                {
                    r.Id
                })
                ;
            modelBuilder.Entity<AspNetUserRole>()
                .HasKey(r => new
                {
                    r.RoleId,
                    r.UserId
                })
                ;

            modelBuilder.Entity<AspNetUserLogin>()
                .HasKey(l => new
                {
                    l.LoginProvider,
                    l.ProviderKey,
                    l.UserId
                });

            modelBuilder.Entity<AspNetUserToken>()
                .HasKey(l => new
                {
                    l.UserId,
                    l.LoginProvider,
                    l.Name
                });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
