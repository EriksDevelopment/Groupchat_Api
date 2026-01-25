using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Group>> GetGroupAsync(int userId) =>
            await _context.Groups
                .Where(g => g.GroupUsers.Any(gu => gu.UserId == userId))
                .Include(g => g.GroupUsers)
                .ThenInclude(gu => gu.User)
                .ToListAsync();

        public async Task<Group?> GetInviteCodeAsync(string inviteCode) =>
            await _context.Groups.FirstOrDefaultAsync(g => g.InviteCode == inviteCode);

        public async Task<GroupUser> AddGroupUserAsync(GroupUser groupUser)
        {
            _context.GroupUsers.Add(groupUser);
            await _context.SaveChangesAsync();
            return groupUser;
        }

        public async Task<bool> IsUserInGroupAsync(int userId, int groupId) =>
            await _context.GroupUsers.AnyAsync(gu => gu.UserId == userId && gu.GroupId == groupId);

        public async Task<GroupUser> LeaveGroupAsync(GroupUser groupUser)
        {
            _context.GroupUsers.Remove(groupUser);
            await _context.SaveChangesAsync();
            return groupUser;
        }

        public async Task<GroupUser?> GetGroupUserAsync(int userId, int groupId) =>
            await _context.GroupUsers.FirstOrDefaultAsync(gu => gu.UserId == userId && gu.GroupId == groupId);

        public async Task<Group> DeleteGroupAsync(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<int> CountUsersInGroupAsync(int groupId) =>
            await _context.GroupUsers.CountAsync(gu => gu.GroupId == groupId);
    }
}