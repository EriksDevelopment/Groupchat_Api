using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IGroupRepo
    {
        Task<Group> AddGroupAsync(Group group, int creatorUserId);
        Task<List<Group>> GetGroupAsync(int userId);
        Task<Group?> GetInviteCodeAsync(string inviteCode);
        Task<GroupUser> AddGroupUserAsync(GroupUser groupUser);
        Task<bool> IsUserInGroupAsync(int userId, int groupId);
        Task<GroupUser> LeaveGroupAsync(GroupUser groupUser);
        Task<GroupUser?> GetGroupUserAsync(int userId, int groupId);
        Task<Group> DeleteGroupAsync(Group group);
        Task<int> CountUsersInGroupAsync(int groupId);
    }
}