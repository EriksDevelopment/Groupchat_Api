using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos.Message;
using Groupchat_Api.Data.Interfaces;

namespace Groupchat_Api.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepo _messageRepo;
        private readonly IGroupRepo _groupRepo;
        public MessageService(IMessageRepo messageRepo, IGroupRepo groupRepo)
        {
            _messageRepo = messageRepo;
            _groupRepo = groupRepo;
        }

        public async Task<List<MessageShowResponseDto>> ShowAsync(string inviteCode)
        {
            if (string.IsNullOrWhiteSpace(inviteCode))
                throw new ArgumentException("Invite code can't be empty.");

            var group = await _groupRepo.GetInviteCodeAsync(inviteCode);
            if (group == null)
                throw new ArgumentException("No group found.");

            var messages = await _messageRepo.GetMessageAsync(inviteCode);
            if (!messages.Any())
                throw new ArgumentException("No messages in groupchat.");

            return messages.Select(m => new MessageShowResponseDto
            {
                UserName = m.User.UserName,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
        }
    }
}