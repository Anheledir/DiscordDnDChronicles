namespace DiscordDnDChronicles.Core.Domain.Models;

public class Location
{
    // Primary key
    public int Id { get; set; }

    // The name of the location (e.g., "Baldur's Gate")
    public required string Name { get; set; }

    // A description of the location
    public string? Description { get; set; }

    // A slug derived from the name (e.g., "baldurs-gate")
    public required string Slug { get; set; }

    // Foreign key for the associated campaign
    public int CampaignId { get; set; }

    // Navigation property to the campaign
    public required Campaign Campaign { get; set; }

    // Navigation property to maps associated with this location.
    // The Map model should be defined separately if needed.
    public ICollection<Map> Maps { get; set; } = [];
}
