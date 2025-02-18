namespace DiscordDnDChronicles.Core.Domain.Models;

public enum CharacterType
{
    PC, // Player Character
    NPC // Non-Player Character (managed by the DM)
}

public class Character
{
    // Primary key
    public int Id { get; set; }

    // The character's in-game name
    public required string Name { get; set; }

    // Indicates whether this is a PC or an NPC
    public CharacterType Type { get; set; }

    // Optional: A brief description or backstory for the character
    public string? Description { get; set; }

    // Foreign key for the associated campaign
    public int CampaignId { get; set; }

    // Navigation property to the campaign this character belongs to
    public required Campaign Campaign { get; set; }

    // Navigation property to character groups (e.g., guilds, vendors)
    public ICollection<CharacterGroup> Groups { get; set; } = [];

    // Navigation property for the associated Discord users
    public ICollection<DiscordUser> DiscordUsers { get; set; } = [];
}
