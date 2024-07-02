using DeckStats.Data.Models;
using DeckStats.Data.StaticData;
using Microsoft.EntityFrameworkCore;

namespace DeckStats.Data;

public class DeckStatsDbContext : DbContext
{
    public DeckStatsDbContext() { }
    public DeckStatsDbContext(DbContextOptions<DeckStatsDbContext> options) : base(options) { }
    

    public DbSet<Deck> Decks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("DeckStatsDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Deck>().HasData(DeckStaticData.GetDeckStaticData());
        modelBuilder.Entity<User>().HasData(UserStaticData.GetUserStaticData());
    }
}
