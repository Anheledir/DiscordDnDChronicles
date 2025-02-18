using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface ICampaignRepository
{
    /// <summary>
    /// Retrieves a campaign by its unique identifier.
    /// </summary>
    /// <param name="id">The unique campaign identifier.</param>
    /// <returns>The campaign entity if found, otherwise null.</returns>
    Task<Campaign> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all campaigns.
    /// </summary>
    /// <returns>An enumerable collection of campaigns.</returns>
    Task<IEnumerable<Campaign>> GetAllAsync();

    /// <summary>
    /// Adds a new campaign.
    /// </summary>
    /// <param name="campaign">The campaign entity to add.</param>
    Task AddAsync(Campaign campaign);

    /// <summary>
    /// Updates an existing campaign.
    /// </summary>
    /// <param name="campaign">The campaign entity with updated values.</param>
    Task UpdateAsync(Campaign campaign);

    /// <summary>
    /// Deletes a campaign.
    /// </summary>
    /// <param name="campaign">The campaign entity to delete.</param>
    Task DeleteAsync(Campaign campaign);
}
