namespace DiscordDnDChronicles.Core.Domain.Models;

public class Message
{
    // Primary key in our database
    public int Id { get; set; }

    // The unique ID of the message as provided by Discord
    public required string DiscordMessageId { get; set; }

    // The type of the message (e.g., "Default")
    public required string Type { get; set; }

    // The timestamp when the message was created
    public DateTime Timestamp { get; set; }

    // Optional: The timestamp when the message was edited (if applicable)
    public DateTime? TimestampEdited { get; set; }

    // Indicates whether the message was pinned
    public bool IsPinned { get; set; }

    // The actual text content of the message
    public string? Content { get; set; }

    // The Discord user ID of the message's author
    public required string AuthorDiscordId { get; set; }

    // The display name or nickname of the author at the time of the message
    public required string AuthorName { get; set; }

    // Foreign key to the associated channel
    public int ChannelId { get; set; }

    // Navigation property to the Channel this message belongs to
    public required Channel Channel { get; set; }
}
