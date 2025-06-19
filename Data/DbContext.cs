using CourseStudent.Models;

namespace CourseStudent.Data;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
    {
        public DbSet<Character>       Characters       => Set<Character>();
        public DbSet<Item>            Items            => Set<Item>();
        public DbSet<Backpack>        Backpacks        => Set<Backpack>();
        public DbSet<Title>           Titles           => Set<Title>();
        public DbSet<CharacterTitle>  CharacterTitles  => Set<CharacterTitle>();

        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Backpack>()
              .HasKey(b => new { b.CharacterId, b.ItemId });
            mb.Entity<Backpack>()
              .HasOne(b => b.Character)
              .WithMany(c => c.BackpackItems)
              .HasForeignKey(b => b.CharacterId);
            mb.Entity<Backpack>()
              .HasOne(b => b.Item)
              .WithMany(i => i.Backpacks)
              .HasForeignKey(b => b.ItemId);

            mb.Entity<CharacterTitle>()
              .HasKey(ct => new { ct.CharacterId, ct.TitleId });
            mb.Entity<CharacterTitle>()
              .HasOne(ct => ct.Character)
              .WithMany(c => c.Titles)
              .HasForeignKey(ct => ct.CharacterId);
            mb.Entity<CharacterTitle>()
              .HasOne(ct => ct.Title)
              .WithMany(t => t.CharacterTitles)
              .HasForeignKey(ct => ct.TitleId);


            mb.Entity<Character>().HasData(new Character {
                CharacterId   = 1,
                FirstName     = "John",
                LastName      = "Yakuza",
                CurrentWeight = 43,
                MaxWeight     = 200
            });

            mb.Entity<Item>().HasData(
                new Item { ItemId = 1, Name = "Item1", Weight = 10 },
                new Item { ItemId = 2, Name = "Item2", Weight = 11 },
                new Item { ItemId = 3, Name = "Item3", Weight = 12 }
            );

            mb.Entity<Title>().HasData(
                new Title { TitleId = 1, Name = "Title1" },
                new Title { TitleId = 2, Name = "Title2" },
                new Title { TitleId = 3, Name = "Title3" }
            );

            mb.Entity<Backpack>().HasData(
                new { CharacterId = 1, ItemId = 1, Amount = 2 },
                new { CharacterId = 1, ItemId = 2, Amount = 1 },
                new { CharacterId = 1, ItemId = 3, Amount = 1 }
            );

            mb.Entity<CharacterTitle>().HasData(
                new { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2024, 6, 10) },
                new { CharacterId = 1, TitleId = 2, AcquiredAt = new DateTime(2024, 6,  9) },
                new { CharacterId = 1, TitleId = 3, AcquiredAt = new DateTime(2024, 6,  8) }
            );
        }
    }