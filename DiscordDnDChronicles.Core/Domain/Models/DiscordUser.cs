namespace DiscordDnDChronicles.Core.Domain.Models;

public class DiscordUser
{
    // Primary key for our database
    public int Id { get; set; }

    // The unique Discord user identifier (UID)
    public required string DiscordUserId { get; set; }

    // The user's nickname or display name
    public required string Nickname { get; set; }

    // Navigation property for the associated campaign characters.
    // This establishes a many-to-many relationship with the Character entity.
    public ICollection<Character> Characters { get; set; } = [];
}
