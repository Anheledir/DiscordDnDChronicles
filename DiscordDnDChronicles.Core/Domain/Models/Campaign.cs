namespace DiscordDnDChronicles.Core.Domain.Models;

public class Campaign
{
    // Primary key
    public int Id { get; set; }

    // Campaign name
    public required string Name { get; set; }

    // A brief description of the campaign
    public string? Description { get; set; }

    // The date the campaign started
    public DateTime StartDate { get; set; }

    // Optional end date if the campaign has concluded
    public DateTime? EndDate { get; set; }

    // Navigation property to associated locations (cities, dungeons, etc.)
    public ICollection<Location> Locations { get; set; } = [];

    // Navigation property to associated characters (PCs and NPCs)
    public ICollection<Character> Characters { get; set; } = [];

    // Navigation property to associated messages (roleplay content)
    public ICollection<Message> Messages { get; set; } = [];
}
