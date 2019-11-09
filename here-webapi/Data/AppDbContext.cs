using here_webapi.Models.DersModeller;
using here_webapi.Models.Identity;
using here_webapi.Models.Kurumlar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Data
{
    public class AppDbContext : DbContext
    {
        #region KurumModelleri
        public DbSet<Universite> Universiteler { get; set; }
        public DbSet<Fakulte> Fakulteler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        #endregion

        #region IdentityModelleri
        public DbSet<AppUser> Users { get; set; }
        #endregion

        #region DersModelleri
        public DbSet<Ders> Dersler { get; set; }
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
            modelBuilder.Entity<AppUser>(u =>
            {
                u.HasMany(x => x.VerilenDersler).WithOne(d => d.Ogretmen).HasForeignKey(w => w.OgretmenId);
                u.HasMany(x => x.AlinanDersler).WithOne(d => d.Ogrenci).HasForeignKey(o => o.OgrenciId);
            });
            #endregion

            #region DersModelAyarlari

            modelBuilder.Entity<Ders>(d => 
            {
                d.HasOne(x => x.Ogretmen).WithMany(o => o.VerilenDersler).HasForeignKey(y => y.OgretmenId);
                d.HasMany(x => x.DersiAlanlar).WithOne(a => a.Ders).HasForeignKey(e => e.DersId);
            });
            modelBuilder.Entity<AlinanDersler>(d =>
            {
                d.HasOne(x => x.Ogrenci).WithMany(o => o.AlinanDersler).HasForeignKey(x => x.OgrenciId);
                d.HasOne(x => x.Ders).WithMany(w => w.DersiAlanlar).HasForeignKey(x => x.DersId);
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
