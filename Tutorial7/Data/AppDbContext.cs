using Microsoft.EntityFrameworkCore;
using Tutorial7.Models;

namespace Tutorial7.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PC> PCs { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<PCComponent> PCComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PC>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).HasMaxLength(50).IsRequired();
                e.Property(p => p.CreatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ComponentType>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Abbreviation).HasMaxLength(30).IsRequired();
                e.Property(c => c.Name).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<ComponentManufacturer>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Abbreviation).HasMaxLength(30).IsRequired();
                e.Property(c => c.FullName).HasMaxLength(300).IsRequired();
                e.Property(c => c.FoundationDate).HasColumnType("date");
            });

            modelBuilder.Entity<Component>(e =>
            {
                e.HasKey(c => c.Code);
                e.Property(c => c.Code).HasColumnType("char(10)");
                e.Property(c => c.Name).HasMaxLength(300).IsRequired();
                e.Property(c => c.Description).HasColumnType("nvarchar(max)");
                
                e.HasOne(c => c.Manufacturer)
                 .WithMany(m => m.Components)
                 .HasForeignKey(c => c.ComponentManufacturersId);

                e.HasOne(c => c.Type)
                 .WithMany(t => t.Components)
                 .HasForeignKey(c => c.ComponentTypesId);
            });

            modelBuilder.Entity<PCComponent>(e =>
            {
                e.HasKey(pc => new { pc.PCId, pc.ComponentCode }); 
                e.Property(pc => pc.ComponentCode).HasColumnType("char(10)");

                e.HasOne(pc => pc.PC)
                 .WithMany(p => p.PCComponents)
                 .HasForeignKey(pc => pc.PCId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(pc => pc.Component)
                 .WithMany(c => c.PCComponents)
                 .HasForeignKey(pc => pc.ComponentCode)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ComponentType>().HasData(
                new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
                new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
                new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
            );

            modelBuilder.Entity<ComponentManufacturer>().HasData(
                new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
                new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
                new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) }
            );

            modelBuilder.Entity<Component>().HasData(
                new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
                new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
                new Component { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 }
            );

            modelBuilder.Entity<PC>().HasData(
                new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5f, Warranty = 36, CreatedAt = DateTime.Parse("2026-05-08T09:00:00"), Stock = 5 },
                new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2f, Warranty = 24, CreatedAt = DateTime.Parse("2026-04-15T13:30:00"), Stock = 12 },
                new PC { Id = 3, Name = "Home Media Center", Weight = 6.0f, Warranty = 12, CreatedAt = DateTime.Parse("2026-05-10T10:00:00"), Stock = 3 }
            );

            modelBuilder.Entity<PCComponent>().HasData(
                new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
                new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
                new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
                new PCComponent { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 }
            );
        }
    }
}