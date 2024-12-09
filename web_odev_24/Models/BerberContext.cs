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

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Berber24;Trusted_Connection=True;");

        }
    }
}

