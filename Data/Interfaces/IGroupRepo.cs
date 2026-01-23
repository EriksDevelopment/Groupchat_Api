using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IGroupRepo
    {
        Task<Group> AddGroupAsync(Group group, int creatorUserId);
        Task<List<Group>> GetGroupAsync();
    }
}