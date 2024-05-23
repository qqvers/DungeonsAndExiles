using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace DungeonsAndExiles.Api.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Backpack> Backpacks { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(b => b.Backpack)
                .WithOne(p => p.Player)
                .HasForeignKey<Backpack>(b => b.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                .HasOne(e => e.Equipment)
                .WithOne(p => p.Player)
                .HasForeignKey<Equipment>(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(b => b.Players)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Backpack>()
                .HasMany(i => i.Items)
                .WithOne(b => b.Backpack)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Equipment>()
                .HasMany(i => i.Items)
                .WithOne(e => e.Equipment)
                .OnDelete(DeleteBehavior.Cascade);


            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "User" }
            };
            modelBuilder.Entity<Role>().HasData(roles);

            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "Admin", Email = "admin@admin.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), RoleId = roles[0].Id },
                new User { Id = Guid.NewGuid(), Name = "John", Email = "john@doe.com", Password = BCrypt.Net.BCrypt.HashPassword("johndoe"), RoleId = roles[1].Id },
                new User { Id = Guid.NewGuid(), Name = "Chris", Email = "chris@wilson.com", Password = BCrypt.Net.BCrypt.HashPassword("chriswilson"), RoleId = roles[1].Id },
            };
            modelBuilder.Entity<User>().HasData(users);


        }
    }
}
