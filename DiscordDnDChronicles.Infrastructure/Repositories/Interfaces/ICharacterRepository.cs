using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface ICharacterRepository
{
    /// <summary>
    /// Retrieves a character by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns>The character entity if found, otherwise null.</returns>
    Task<Character> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all characters.
    /// </summary>
    /// <returns>An enumerable collection of characters.</returns>
    Task<IEnumerable<Character>> GetAllAsync();

    /// <summary>
    /// Retrieves all characters associated with a specific campaign.
    /// </summary>
    /// <param name="campaignId">The unique identifier of the campaign.</param>
    /// <returns>An enumerable collection of characters belonging to the campaign.</returns>
    Task<IEnumerable<Character>> GetByCampaignIdAsync(int campaignId);

    /// <summary>
    /// Adds a new character.
    /// </summary>
    /// <param name="character">The character entity to add.</param>
    Task AddAsync(Character character);

    /// <summary>
    /// Updates an existing character.
    /// </summary>
    /// <param name="character">The character entity with updated values.</param>
    Task UpdateAsync(Character character);

    /// <summary>
    /// Deletes a character.
    /// </summary>
    /// <param name="character">The character entity to delete.</param>
    Task DeleteAsync(Character character);
}
