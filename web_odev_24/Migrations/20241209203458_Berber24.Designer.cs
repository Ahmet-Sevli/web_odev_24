﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_odev_24.Models;

#nullable disable

namespace web_odev_24.Migrations
{
    [DbContext(typeof(BerberContext))]
    [Migration("20241209203458_Berber24")]
    partial class Berber24
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("web_odev_24.Models.Admin", b =>
                {
                    b.Property<int>("adminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("adminID"));

                    b.Property<string>("admin_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("adminID");

                    b.ToTable("Adminler");
                });

            modelBuilder.Entity("web_odev_24.Models.Calisan", b =>
                {
                    b.Property<int>("calisanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("calisanID"));

                    b.Property<string>("calisan_ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("calisan_soyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("calisan_tecrube")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("islemID")
                        .HasColumnType("int");

                    b.HasKey("calisanID");

                    b.HasIndex("islemID");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("web_odev_24.Models.Islem", b =>
                {
                    b.Property<int>("islemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("islemID"));

                    b.Property<string>("islem_ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("islem_sure")
                        .HasColumnType("int");

                    b.Property<int>("islem_ucret")
                        .HasColumnType("int");

                    b.HasKey("islemID");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("web_odev_24.Models.Musteri", b =>
                {
                    b.Property<int>("musteriID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("musteriID"));

                    b.Property<string>("musteri_ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("musteri_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("musteri_sifre")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("musteri_soyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("musteri_telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("musteriID");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("web_odev_24.Models.Calisan", b =>
                {
                    b.HasOne("web_odev_24.Models.Islem", "islem")
                        .WithMany("Calisanlar")
                        .HasForeignKey("islemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("islem");
                });

            modelBuilder.Entity("web_odev_24.Models.Islem", b =>
                {
                    b.Navigation("Calisanlar");
                });
#pragma warning restore 612, 618
        }
    }
}
