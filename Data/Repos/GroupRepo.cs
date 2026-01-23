using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Repos
{
    public class GroupRepo : IGroupRepo
    {
        private readonly GroupchatDbContext _context;
        public GroupRepo(GroupchatDbContext context)
        {
            _context = context;
        }

        public async Task<Group> AddGroupAsync(Group group, int creatorUserId)
        {
            group.GroupUsers.Add(new GroupUser
            {
                UserId = creatorUserId,
                IsAdmin = true,
                JoinedAt = DateTime.UtcNow
            });

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }
    }
}