using DecorStudio_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Warehouse_Decor>()
                .HasOne(w => w.Warehouse)
                .WithMany(w => w.Warehouse_Decors)
                .HasForeignKey(w => w.WarehouseId);

            modelBuilder.Entity<Warehouse_Decor>()
                .HasOne(d => d.Decor)
                .WithMany(d => d.Warehouse_Decors)
                .HasForeignKey(d => d.DecorId);

            modelBuilder.Entity<Catalog_Decor>()
                .HasOne(c => c.Catalog)
                .WithMany(c => c.Catalog_Decors)
                .HasForeignKey(c => c.CatalogId);

            modelBuilder.Entity<Catalog_Decor>()
                .HasOne(d => d.Decor)
                .WithMany(d => d.Catalog_Decors)
                .HasForeignKey(d => d.DecorId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Decor_Reservation>()
                .HasOne(r => r.Decor)
                .WithMany(r => r.Decor_Reservations)
                .HasForeignKey(r => r.DecorId);

            modelBuilder.Entity<Decor_Reservation>()
                .HasOne(r => r.Reservation)
                .WithMany(r => r.Decor_Reservations)
                .HasForeignKey(r => r.ReservationId);
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Decor> Decors { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Warehouse_Decor> Warehouse_Decors { get; set; }
        public DbSet<Catalog_Decor> Catalog_Decors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Decor_Reservation> Decor_Reservations { get; set; }
    }
}
