using Groupchat_Api.Data.Dtos.Message;

namespace Groupchat_Api.Core.Interfaces
{
    public interface IMessageService
    {
        Task<List<ShowResponseDto>> ShowAsync(string inviteCode);
    }
}