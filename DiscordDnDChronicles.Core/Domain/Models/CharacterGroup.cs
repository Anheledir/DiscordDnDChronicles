namespace DiscordDnDChronicles.Core.Domain.Models;

public class CharacterGroup
{
    // Primary key
    public int Id { get; set; }

    // The name of the group (e.g., "Adventurers Guild", "Merchants", etc.)
    public required string Name { get; set; }

    // A brief description of the group
    public string? Description { get; set; }

    // Navigation property to the characters that belong to this group.
    // This establishes a many-to-many relationship with the Character entity.
    public ICollection<Character> Characters { get; set; } = [];
}
