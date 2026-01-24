using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IMessageRepo
    {
        Task<List<Message>> GetMessageAsync(string inviteCode);
    }
}