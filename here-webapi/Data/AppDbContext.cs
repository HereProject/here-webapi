using here_webapi.Models.Identity;
using here_webapi.Models.Kurumlar;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Data
{
    public class AppDbContext : IdentityDbContext
        <
            AppUser,
            AppRole,
            int,
            AppUserClaim,
            AppUserRole,
            AppUserLogin,
            AppRoleClaim,
            AppUserToken
        >
    {
        #region KurumModelleri
        public DbSet<Universite> Universiteler { get; set; }
        public DbSet<Fakulte> Fakulteler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        #endregion
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region KurumModelAyarlari

            modelBuilder.Entity<Universite>(u =>
            {
                u.HasMany(x => x.Fakulteler).WithOne(f => f.Universite);
            });

            modelBuilder.Entity<Fakulte>(u =>
            {
                u.HasMany(x => x.Bolumler).WithOne(f => f.Fakulte);
            });

            #endregion

            #region IdentityAyarlari
            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<AppUser>(b =>
            {
                b.ToTable("identity_users");
            });

            modelBuilder.Entity<AppUserClaim>(b =>
            {
                b.ToTable("identity_userclaims");
            });

            modelBuilder.Entity<AppUserLogin>(b =>
            {
                b.ToTable("identity_userlogins");
            });

            modelBuilder.Entity<AppUserToken>(b =>
            {
                b.ToTable("identity_usertokens");
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.ToTable("identity_roles");
            });

            modelBuilder.Entity<AppRoleClaim>(b =>
            {
                b.ToTable("identity_roleclaims");
            });

            modelBuilder.Entity<AppUserRole>(b =>
            {
                b.ToTable("identity_userroles");
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
