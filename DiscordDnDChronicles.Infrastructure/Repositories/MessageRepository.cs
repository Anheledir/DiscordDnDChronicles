using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class MessageRepository(AppDbContext _context) : IMessageRepository
{
    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Message message)
    {
        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Message>> GetAllAsync() =>
        await _context.Messages.ToListAsync();

    public async Task<Message> GetByIdAsync(int id) =>
        await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Message> GetByDiscordMessageIdAsync(string discordMessageId) =>
        await _context.Messages.FirstOrDefaultAsync(m => m.DiscordMessageId == discordMessageId);

    public async Task<IEnumerable<Message>> GetByChannelIdAsync(int channelId) =>
        await _context.Messages.Where(m => m.ChannelId == channelId).ToListAsync();

    public async Task UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }
}
