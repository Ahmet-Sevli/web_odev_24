using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

namespace web_odev_24.Models
{
    public class BerberContext : DbContext
    {
        public BerberContext(DbContextOptions<BerberContext> options) : base(options)  // DbContext'e seçenekleri geçiriyoruz

        {
        }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Islem> Islemler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // İşlem ve Çalışan arasındaki ilişki
            modelBuilder.Entity<Islem>()
                .HasOne<Calisan>() // İlgili sınıf
                .WithMany()        // Bir çalışanın birden fazla işlem kaydı olabilir
                .HasForeignKey(i => i.calisanID) // Foreign key
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete (isteğe bağlı)

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Berber24;Trusted_Connection=True;");

        }
    }
}

