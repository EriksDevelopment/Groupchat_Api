using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Groupchat_Api.Data.Repos
{
    public class MessageRepo : IMessageRepo
    {
        private readonly GroupchatDbContext _context;
        public MessageRepo(GroupchatDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessageAsync(string inviteCode) =>
            await _context.Messages
            .Where(m => m.Group.InviteCode == inviteCode)
            .Include(m => m.User)
            .ToListAsync();
    }
}