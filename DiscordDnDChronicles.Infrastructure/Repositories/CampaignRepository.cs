using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class CampaignRepository(AppDbContext context) : ICampaignRepository
{
    public async Task AddAsync(Campaign campaign)
    {
        await context.Campaigns.AddAsync(campaign);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Campaign campaign)
    {
        context.Campaigns.Remove(campaign);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Campaign>> GetAllAsync()
    {
        return await context.Campaigns
                             .Include(c => c.Characters)
                             .Include(c => c.Locations)
                             .Include(c => c.Messages)
                             .ToListAsync();
    }

    public async Task<Campaign> GetByIdAsync(int id)
    {
        return await context.Campaigns
                             .Include(c => c.Characters)
                             .Include(c => c.Locations)
                             .Include(c => c.Messages)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateAsync(Campaign campaign)
    {
        context.Campaigns.Update(campaign);
        await context.SaveChangesAsync();
    }
}
