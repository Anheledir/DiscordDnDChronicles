using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface IDiscordUserRepository
{
    /// <summary>
    /// Retrieves a DiscordUser by its internal database ID.
    /// </summary>
    /// <param name="id">The internal database identifier.</param>
    /// <returns>The DiscordUser entity if found, otherwise null.</returns>
    Task<DiscordUser> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a DiscordUser by its unique Discord user ID.
    /// </summary>
    /// <param name="discordUserId">The unique Discord user ID as a string.</param>
    /// <returns>The DiscordUser entity if found, otherwise null.</returns>
    Task<DiscordUser> GetByDiscordUserIdAsync(string discordUserId);

    /// <summary>
    /// Retrieves all DiscordUser entities.
    /// </summary>
    /// <returns>An enumerable collection of DiscordUser entities.</returns>
    Task<IEnumerable<DiscordUser>> GetAllAsync();

    /// <summary>
    /// Adds a new DiscordUser entity.
    /// </summary>
    /// <param name="discordUser">The DiscordUser entity to add.</param>
    Task AddAsync(DiscordUser discordUser);

    /// <summary>
    /// Updates an existing DiscordUser entity.
    /// </summary>
    /// <param name="discordUser">The DiscordUser entity with updated values.</param>
    Task UpdateAsync(DiscordUser discordUser);

    /// <summary>
    /// Deletes a DiscordUser entity.
    /// </summary>
    /// <param name="discordUser">The DiscordUser entity to delete.</param>
    Task DeleteAsync(DiscordUser discordUser);
}
