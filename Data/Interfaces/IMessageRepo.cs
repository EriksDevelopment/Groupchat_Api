using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IMessageRepo
    {
        Task<List<Message>> GetMessageAsync(string inviteCode);
        Task<Message> AddMessageAsync(Message message);
        Task SetMessageToUnknownAsync(int userId, int groupId);
    }
}