using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class ChannelRepository(AppDbContext context) : IChannelRepository
{
    public async Task AddAsync(Channel channel)
    {
        await context.Channels.AddAsync(channel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Channel channel)
    {
        context.Channels.Remove(channel);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Channel>> GetAllAsync()
    {
        return await context.Channels.ToListAsync();
    }

    public async Task<Channel> GetByDiscordChannelIdAsync(string discordChannelId) => await context.Channels.FirstOrDefaultAsync(c => c.DiscordChannelId == discordChannelId);

    public async Task<Channel> GetByIdAsync(int id) => await context.Channels.FirstOrDefaultAsync(c => c.Id == id);

    public async Task UpdateAsync(Channel channel)
    {
        context.Channels.Update(channel);
        await context.SaveChangesAsync();
    }
}
