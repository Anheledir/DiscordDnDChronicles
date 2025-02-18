namespace DiscordDnDChronicles.Core.Domain.Models;

public class Channel
{
    // Primary key for our database
    public int Id { get; set; }

    // The unique ID of the Discord channel (as provided in the export)
    public required string DiscordChannelId { get; set; }

    // The name of the channel (e.g. "faryn-isles")
    public required string Name { get; set; }

    // The type of the channel (e.g. "GuildTextChat")
    public required string Type { get; set; }

    // The ID of the channel's category in Discord
    public string? CategoryId { get; set; }

    // The name of the category (e.g. a path like "\\DnD\\The Shadows of the Lamb and Wolf\\")
    public string? Category { get; set; }

    // Optional topic or description of the channel
    public string? Topic { get; set; }

    public int CampaignId { get; set; }
    public required Campaign Campaign { get; set; }
}
