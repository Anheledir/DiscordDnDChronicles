namespace DiscordDnDChronicles.Core.Domain.Models;

public class Map
{
    // Primary key
    public int Id { get; set; }

    // Title or name for the map (e.g., "City Layout", "Dungeon Map")
    public required string Title { get; set; }

    // Optional description for the map
    public string? Description { get; set; }

    // URL or file path to the map image/file
    public required string Url { get; set; }

    // Foreign key to the associated Location
    public int LocationId { get; set; }

    // Navigation property to the Location this map belongs to
    public required Location Location { get; set; }
}
