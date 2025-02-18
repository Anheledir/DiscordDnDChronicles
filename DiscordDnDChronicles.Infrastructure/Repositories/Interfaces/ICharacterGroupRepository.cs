using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface ICharacterGroupRepository
{
    /// <summary>
    /// Retrieves a CharacterGroup by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the group.</param>
    /// <returns>The CharacterGroup entity if found, otherwise null.</returns>
    Task<CharacterGroup> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all CharacterGroups.
    /// </summary>
    /// <returns>An enumerable collection of CharacterGroups.</returns>
    Task<IEnumerable<CharacterGroup>> GetAllAsync();

    /// <summary>
    /// Retrieves a CharacterGroup by its name.
    /// </summary>
    /// <param name="name">The name of the group.</param>
    /// <returns>The CharacterGroup entity if found, otherwise null.</returns>
    Task<CharacterGroup> GetByNameAsync(string name);

    /// <summary>
    /// Adds a new CharacterGroup.
    /// </summary>
    /// <param name="group">The CharacterGroup entity to add.</param>
    Task AddAsync(CharacterGroup group);

    /// <summary>
    /// Updates an existing CharacterGroup.
    /// </summary>
    /// <param name="group">The CharacterGroup entity with updated values.</param>
    Task UpdateAsync(CharacterGroup group);

    /// <summary>
    /// Deletes a CharacterGroup.
    /// </summary>
    /// <param name="group">The CharacterGroup entity to delete.</param>
    Task DeleteAsync(CharacterGroup group);
}
