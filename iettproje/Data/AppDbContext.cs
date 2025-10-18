using iettproje.Models;
using iettproje;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iettproje.Data
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<IslemlerList> IslemlerList { get; set; }
        public DbSet<UrunKategorileri> UrunKategorileri { get; set; }
        public DbSet<UrunModelleri> UrunModelleri {  get; set; } 

    }

}