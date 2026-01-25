using System.Runtime.InteropServices.Marshalling;
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
            .OrderByDescending(m => m.SentAt)
            .ToListAsync();

        public async Task<Message> AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task SetMessageToUnknownAsync(int userId, int groupId)
        {
            var messages = await _context.Messages
                .Where(m => m.UserId == userId && m.GroupId == groupId)
                .ToListAsync();

            foreach (var m in messages)
            {
                m.UserId = null;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Message> DeleteMessageAsync(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}