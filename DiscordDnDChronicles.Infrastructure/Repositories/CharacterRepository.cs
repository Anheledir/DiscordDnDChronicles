using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class CharacterRepository(AppDbContext context) : ICharacterRepository
{
    public async Task AddAsync(Character character)
    {
        await context.Characters.AddAsync(character);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Character character)
    {
        context.Characters.Remove(character);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Character>> GetAllAsync()
    {
        return await context.Characters
                             .Include(c => c.Campaign)
                             .Include(c => c.Groups)
                             .Include(c => c.DiscordUsers)
                             .ToListAsync();
    }

    public async Task<Character> GetByIdAsync(int id)
    {
        return await context.Characters
                             .Include(c => c.Campaign)
                             .Include(c => c.Groups)
                             .Include(c => c.DiscordUsers)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Character>> GetByCampaignIdAsync(int campaignId)
    {
        return await context.Characters
                             .Include(c => c.Campaign)
                             .Include(c => c.Groups)
                             .Include(c => c.DiscordUsers)
                             .Where(c => c.CampaignId == campaignId)
                             .ToListAsync();
    }

    public async Task UpdateAsync(Character character)
    {
        context.Characters.Update(character);
        await context.SaveChangesAsync();
    }
}
