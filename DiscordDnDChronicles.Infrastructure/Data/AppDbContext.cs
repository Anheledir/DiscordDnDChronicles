using DiscordDnDChronicles.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterGroup> CharacterGroups { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Map> Maps { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<DiscordUser> DiscordUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure many-to-many between Characters and DiscordUsers
        modelBuilder.Entity<Character>()
            .HasMany(c => c.DiscordUsers)
            .WithMany(u => u.Characters)
            .UsingEntity(j => j.ToTable("CharacterDiscordUsers"));

        // Configure many-to-many between Characters and CharacterGroups
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Groups)
            .WithMany(g => g.Characters)
            .UsingEntity(j => j.ToTable("CharacterGroupsCharacters"));

        // Campaign configuration
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(c => c.Description)
                  .HasMaxLength(1000);
        });

        // Channel configuration
        modelBuilder.Entity<Channel>(entity =>
        {
            entity.Property(c => c.DiscordChannelId)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.HasIndex(c => c.DiscordChannelId).IsUnique();
            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Character configuration
        modelBuilder.Entity<Character>(entity =>
        {
            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.HasOne(c => c.Campaign)
                  .WithMany(camp => camp.Characters)
                  .HasForeignKey(c => c.CampaignId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // CharacterGroup configuration
        modelBuilder.Entity<CharacterGroup>(entity =>
        {
            entity.Property(g => g.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Location configuration
        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(l => l.Name)
                  .IsRequired()
                  .HasMaxLength(150);
            entity.Property(l => l.Slug)
                  .IsRequired()
                  .HasMaxLength(150);
            entity.HasIndex(l => l.Slug).IsUnique();
        });

        // Map configuration
        modelBuilder.Entity<Map>(entity =>
        {
            entity.Property(m => m.Title)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(m => m.Url)
                  .IsRequired()
                  .HasMaxLength(500);
        });

        // Message configuration
        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(m => m.DiscordMessageId)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.HasIndex(m => m.DiscordMessageId).IsUnique();
            entity.Property(m => m.Content)
                  .IsRequired();
            entity.Property(m => m.Timestamp)
                  .IsRequired();
        });

        // DiscordUser configuration
        modelBuilder.Entity<DiscordUser>(entity =>
        {
            entity.Property(u => u.DiscordUserId)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.HasIndex(u => u.DiscordUserId).IsUnique();
            entity.Property(u => u.Nickname)
                  .IsRequired()
                  .HasMaxLength(100);
        });
    }
}
