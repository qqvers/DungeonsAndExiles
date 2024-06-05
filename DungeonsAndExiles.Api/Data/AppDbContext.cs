using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace DungeonsAndExiles.Api.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Backpack> Backpacks { get; set; } = null!;
        public virtual DbSet<Equipment> Equipments { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Monster> Monsters { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Backpack)
                .WithOne(b => b.Player)
                .HasForeignKey<Backpack>(b => b.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Player>()
                .HasOne(p => p.Equipment)
                .WithOne(e => e.Player)
                .HasForeignKey<Equipment>(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Players)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Backpack>()
                .HasMany(b => b.Items)
                .WithMany(i => i.Backpacks)
                .UsingEntity<Dictionary<string, object>>(
                    "BackpackItem",
                    r => r.HasOne<Item>().WithMany().HasForeignKey("ItemId"),
                    l => l.HasOne<Backpack>().WithMany().HasForeignKey("BackpackId")
                );


            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Items)
                .WithMany(i => i.Equipments)
                .UsingEntity<Dictionary<string, object>>(
                    "EquipmentItem",
                    r => r.HasOne<Item>().WithMany().HasForeignKey("ItemId"),
                    l => l.HasOne<Equipment>().WithMany().HasForeignKey("EquipmentId")
                );


            //roles
            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "User" }
            };
            modelBuilder.Entity<Role>().HasData(roles);

            //users
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "Admin", Email = "admin@admin.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), RoleId = roles[0].Id },
                new User { Id = Guid.NewGuid(), Name = "John", Email = "john@doe.com", Password = BCrypt.Net.BCrypt.HashPassword("johndoe"), RoleId = roles[1].Id },
                new User { Id = Guid.NewGuid(), Name = "Chris", Email = "chris@wilson.com", Password = BCrypt.Net.BCrypt.HashPassword("chriswilson"), RoleId = roles[1].Id },
            };
            modelBuilder.Entity<User>().HasData(users);


            //items
            modelBuilder.Entity<Item>().HasData(
                
                new Item { Id = Guid.NewGuid(), Name = "Basic Gloves", Type = "Gloves", Damage = 0, Defence = 5 },
                new Item { Id = Guid.NewGuid(), Name = "Leather Gloves", Type = "Gloves", Damage = 0, Defence = 10 },
                new Item { Id = Guid.NewGuid(), Name = "Iron Gloves", Type = "Gloves", Damage = 0, Defence = 15 },
                new Item { Id = Guid.NewGuid(), Name = "Steel Gloves", Type = "Gloves", Damage = 0, Defence = 20 },
                new Item { Id = Guid.NewGuid(), Name = "Titanium Gloves", Type = "Gloves", Damage = 0, Defence = 25 },

                
                new Item { Id = Guid.NewGuid(), Name = "Basic Boots", Type = "Boots", Damage = 0, Defence = 5 },
                new Item { Id = Guid.NewGuid(), Name = "Leather Boots", Type = "Boots", Damage = 0, Defence = 10 },
                new Item { Id = Guid.NewGuid(), Name = "Iron Boots", Type = "Boots", Damage = 0, Defence = 15 },
                new Item { Id = Guid.NewGuid(), Name = "Steel Boots", Type = "Boots", Damage = 0, Defence = 20 },
                new Item { Id = Guid.NewGuid(), Name = "Titanium Boots", Type = "Boots", Damage = 0, Defence = 25 },

                
                new Item { Id = Guid.NewGuid(), Name = "Basic Sword", Type = "Weapon", Damage = 10, Defence = 0 },
                new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Type = "Weapon", Damage = 15, Defence = 0 },
                new Item { Id = Guid.NewGuid(), Name = "Steel Sword", Type = "Weapon", Damage = 20, Defence = 0 },
                new Item { Id = Guid.NewGuid(), Name = "Titanium Sword", Type = "Weapon", Damage = 25, Defence = 0 },
                new Item { Id = Guid.NewGuid(), Name = "Diamond Sword", Type = "Weapon", Damage = 30, Defence = 0 },

                
                new Item { Id = Guid.NewGuid(), Name = "Basic Helmet", Type = "Helmet", Damage = 0, Defence = 5 },
                new Item { Id = Guid.NewGuid(), Name = "Leather Helmet", Type = "Helmet", Damage = 0, Defence = 10 },
                new Item { Id = Guid.NewGuid(), Name = "Iron Helmet", Type = "Helmet", Damage = 0, Defence = 15 },
                new Item { Id = Guid.NewGuid(), Name = "Steel Helmet", Type = "Helmet", Damage = 0, Defence = 20 },
                new Item { Id = Guid.NewGuid(), Name = "Titanium Helmet", Type = "Helmet", Damage = 0, Defence = 25 },

                
                new Item { Id = Guid.NewGuid(), Name = "Basic Armor", Type = "Body Armor", Damage = 0, Defence = 10 },
                new Item { Id = Guid.NewGuid(), Name = "Leather Armor", Type = "Body Armor", Damage = 0, Defence = 20 },
                new Item { Id = Guid.NewGuid(), Name = "Iron Armor", Type = "Body Armor", Damage = 0, Defence = 30 },
                new Item { Id = Guid.NewGuid(), Name = "Steel Armor", Type = "Body Armor", Damage = 0, Defence = 40 },
                new Item { Id = Guid.NewGuid(), Name = "Titanium Armor", Type = "Body Armor", Damage = 0, Defence = 50 }
            );

            //monsters
            var basePath = "wwwroot/images/";

            var gladiatorImage = File.ReadAllBytes(basePath + "gladiator_avatar.png");
            var deadeyeImage = File.ReadAllBytes(basePath + "deadeye_avatar.png");
            var assassinImage = File.ReadAllBytes(basePath + "assassin_avatar.png");
            var occultistImage = File.ReadAllBytes(basePath + "occultist_avatar.png");
            var elementalistImage = File.ReadAllBytes(basePath + "elementalist_avatar.png");
            var juggernautImage = File.ReadAllBytes(basePath + "juggernaut_avatar.png");
            var chieftainImage = File.ReadAllBytes(basePath + "chieftain_avatar.png");
            var slayerImage = File.ReadAllBytes(basePath + "slayer_avatar.png");
            var championImage = File.ReadAllBytes(basePath + "champion_avatar.png");

            modelBuilder.Entity<Monster>().HasData(
                new Monster { Id = Guid.NewGuid(), Name = "Gladiator", Level = 1, Health = 30, Defence = 1, Damage = 5, Image = gladiatorImage },
                new Monster { Id = Guid.NewGuid(), Name = "Deadeye", Level = 5, Health = 50, Defence = 5, Damage = 10, Image = deadeyeImage },
                new Monster { Id = Guid.NewGuid(), Name = "Assassin", Level = 10, Health = 70, Defence = 10, Damage = 20, Image = assassinImage },
                new Monster { Id = Guid.NewGuid(), Name = "Occultist", Level = 15, Health = 75, Defence = 15, Damage = 30, Image = occultistImage },
                new Monster { Id = Guid.NewGuid(), Name = "Elementalist", Level = 20, Health = 150, Defence = 30, Damage = 40, Image = elementalistImage },
                new Monster { Id = Guid.NewGuid(), Name = "Juggernaut", Level = 25, Health = 300, Defence = 50, Damage = 100, Image = juggernautImage },
                new Monster { Id = Guid.NewGuid(), Name = "Chieftain", Level = 30, Health = 150, Defence = 30, Damage = 200, Image = chieftainImage },
                new Monster { Id = Guid.NewGuid(), Name = "Slayer", Level = 40, Health = 200, Defence = 35, Damage = 300, Image = slayerImage },
                new Monster { Id = Guid.NewGuid(), Name = "Champion", Level = 50, Health = 300, Defence = 40, Damage = 400, Image = championImage }
            );

        }
    }
}
