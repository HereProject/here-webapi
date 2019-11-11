﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using here_webapi.Data;

namespace here_webapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191111202456_UserFix")]
    partial class UserFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("here_webapi.Models.DersModels.AlinanDers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DersId");

                    b.Property<int>("OgrenciId");

                    b.HasKey("Id");

                    b.HasIndex("DersId");

                    b.HasIndex("OgrenciId");

                    b.ToTable("AlinanDersler");
                });

            modelBuilder.Entity("here_webapi.Models.DersModels.Ders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BolumId");

                    b.Property<string>("DersAdi")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("OgretmenId");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.HasIndex("OgretmenId");

                    b.ToTable("Dersler");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.Property<int?>("RoleId1");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("BolumId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("FakulteId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int?>("UniversiteId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<byte>("UserType")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AppUser_UserType")
                        .HasDefaultValue((byte)3);

                    b.Property<byte>("_UserType")
                        .HasColumnName("UserType");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.HasIndex("FakulteId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UniversiteId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.Property<int?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.Property<int?>("UserId1");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("RoleId1");

                    b.Property<int?>("UserId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserToken", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId1");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.OgrenciDetay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .HasMaxLength(100);

                    b.Property<string>("Soyad")
                        .HasMaxLength(100);

                    b.Property<string>("TC")
                        .HasMaxLength(11);

                    b.Property<int>("UserId");

                    b.Property<bool>("_Gender")
                        .HasColumnName("Gender");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("OgrenciDetaylari");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.OgretmenDetay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .HasMaxLength(100);

                    b.Property<string>("Soyad")
                        .HasMaxLength(100);

                    b.Property<string>("TC")
                        .HasMaxLength(11);

                    b.Property<int>("UserId");

                    b.Property<bool>("_Gender")
                        .HasColumnName("Gender");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("OgretmenDetaylari");
                });

            modelBuilder.Entity("here_webapi.Models.Kurumlar.Bolum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("FakulteId");

                    b.HasKey("Id");

                    b.HasIndex("FakulteId");

                    b.ToTable("Bolumler");
                });

            modelBuilder.Entity("here_webapi.Models.Kurumlar.Fakulte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("UniversiteId");

                    b.HasKey("Id");

                    b.HasIndex("UniversiteId");

                    b.ToTable("Fakulteler");
                });

            modelBuilder.Entity("here_webapi.Models.Kurumlar.Universite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Universiteler");
                });

            modelBuilder.Entity("here_webapi.Models.DersModels.AlinanDers", b =>
                {
                    b.HasOne("here_webapi.Models.DersModels.Ders", "Ders")
                        .WithMany()
                        .HasForeignKey("DersId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "Ogrenci")
                        .WithMany("AlinanDersler")
                        .HasForeignKey("OgrenciId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("here_webapi.Models.DersModels.Ders", b =>
                {
                    b.HasOne("here_webapi.Models.Kurumlar.Bolum", "Bolum")
                        .WithMany("Dersler")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "Ogretmen")
                        .WithMany("VerilenDersler")
                        .HasForeignKey("OgretmenId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppRoleClaim", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUser", b =>
                {
                    b.HasOne("here_webapi.Models.Kurumlar.Bolum", "Bolum")
                        .WithMany("Kisiler")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("here_webapi.Models.Kurumlar.Fakulte", "Fakulte")
                        .WithMany("Kisiler")
                        .HasForeignKey("FakulteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("here_webapi.Models.Kurumlar.Universite", "Universite")
                        .WithMany("Kisiler")
                        .HasForeignKey("UniversiteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserClaim", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserLogin", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserRole", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("here_webapi.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.AppUserToken", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("here_webapi.Models.Identity.OgrenciDetay", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithOne("OgrenciDetay")
                        .HasForeignKey("here_webapi.Models.Identity.OgrenciDetay", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("here_webapi.Models.Identity.OgretmenDetay", b =>
                {
                    b.HasOne("here_webapi.Models.Identity.AppUser", "User")
                        .WithOne("OgretmenDetay")
                        .HasForeignKey("here_webapi.Models.Identity.OgretmenDetay", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("here_webapi.Models.Kurumlar.Bolum", b =>
                {
                    b.HasOne("here_webapi.Models.Kurumlar.Fakulte", "Fakulte")
                        .WithMany("Bolumler")
                        .HasForeignKey("FakulteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("here_webapi.Models.Kurumlar.Fakulte", b =>
                {
                    b.HasOne("here_webapi.Models.Kurumlar.Universite", "Universite")
                        .WithMany("Fakulteler")
                        .HasForeignKey("UniversiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
