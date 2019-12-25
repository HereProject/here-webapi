using System;
using System.Collections.Generic;
using System.Text;
using here_webapi.Models.DersModels;
using here_webapi.Models.Identity;
using here_webapi.Models.Kurumlar;
using here_webapi.Models.Yoklama;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Here_Web_All.Data
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
        #region UserDetails
        public DbSet<OgrenciDetay> OgrenciDetaylari { get; set; }
        public DbSet<OgretmenDetay> OgretmenDetaylari { get; set; }
        #endregion

        #region KurumModelleri
        public DbSet<Universite> Universiteler { get; set; }
        public DbSet<Fakulte> Fakulteler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        #endregion

        #region DersModelleri
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<AlinanDers> AlinanDersler { get; set; }
        public DbSet<AcilanDers> AcilanDersler { get; set; }
        public DbSet<YoklananOgrenci> YoklananOgrenciler { get; set; }
        #endregion

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region KurumModelAyarlari

            modelBuilder.Entity<Universite>(u =>
            {
                u.HasMany(x => x.Fakulteler).WithOne(f => f.Universite).HasForeignKey(z => z.UniversiteId);
                u.HasMany(x => x.Kisiler).WithOne(f => f.Universite).HasForeignKey(uF => uF.UniversiteId);
            });

            modelBuilder.Entity<Fakulte>(u =>
            {
                u.HasMany(x => x.Bolumler).WithOne(f => f.Fakulte).HasForeignKey(z => z.FakulteId);
                u.HasMany(x => x.Kisiler).WithOne(f => f.Fakulte).HasForeignKey(uF => uF.FakulteId);
            });

            modelBuilder.Entity<Bolum>(u =>
            {
                u.HasMany(x => x.Kisiler).WithOne(f => f.Bolum).HasForeignKey(uF => uF.BolumId);
                u.HasMany(x => x.Dersler).WithOne(f => f.Bolum).HasForeignKey(uF => uF.BolumId);
            });
            #endregion

            #region IdentityAyarlari
            modelBuilder.Entity<AppUser>(b =>
            {
                b.Property(x => x.UserType).HasDefaultValue(UserType.Ogrenci);

                b.HasOne(x => x.OgrenciDetay).WithOne(y => y.User).HasForeignKey<OgrenciDetay>(oD => oD.UserId).OnDelete(DeleteBehavior.Cascade);
                b.HasMany(x => x.AlinanDersler).WithOne(y => y.Ogrenci).HasForeignKey(xy => xy.OgrenciId).OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.OgretmenDetay).WithOne(y => y.User).HasForeignKey<OgretmenDetay>(oD => oD.UserId).OnDelete(DeleteBehavior.Cascade);
                b.HasMany(x => x.VerilenDersler).WithOne(y => y.Ogretmen).HasForeignKey(xy => xy.OgretmenId).OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Bolum).WithMany(x => x.Kisiler).HasForeignKey(x => x.BolumId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Fakulte).WithMany(x => x.Kisiler).HasForeignKey(x => x.FakulteId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Universite).WithMany(x => x.Kisiler).HasForeignKey(x => x.UniversiteId).OnDelete(DeleteBehavior.Restrict);

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

            #region tableNames
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
            #endregion

            #region DersModelAyarlari

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
