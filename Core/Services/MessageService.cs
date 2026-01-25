using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos.Message;
using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;

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

        public async Task<List<MessageShowResponseDto>> ShowAsync(string inviteCode, int userId)
        {
            if (string.IsNullOrWhiteSpace(inviteCode))
                throw new ArgumentException("Invite code can't be empty.");

            var group = await _groupRepo.GetInviteCodeAsync(inviteCode);
            if (group == null)
                throw new ArgumentException("No group found.");

            var isMember = await _groupRepo.IsUserInGroupAsync(userId, group.Id);
            if (!isMember)
                throw new ArgumentException("You are not a member of this group.");

            var messages = await _messageRepo.GetMessageAsync(inviteCode);
            if (!messages.Any())
                throw new ArgumentException("No messages in groupchat.");

            return messages.Select(m => new MessageShowResponseDto
            {
                UserName = m.User == null ? "Unknown" : m.User.UserName,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
        }

        public async Task<MessageCreateResponseDto> CreateAsync(MessageCreateRequestDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.InviteCode) ||
                string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Invite code or content can't be empty.");

            var group = await _groupRepo.GetInviteCodeAsync(dto.InviteCode);
            if (group == null)
                throw new ArgumentException("No group found.");

            var isMember = await _groupRepo.IsUserInGroupAsync(userId, group.Id);
            if (!isMember)
                throw new ArgumentException("You are not a memeber of this group.");

            var message = new Message
            {
                Content = dto.Content,
                SentAt = DateTime.UtcNow,
                GroupId = group.Id,
                UserId = userId
            };

            await _messageRepo.AddMessageAsync(message);

            return new MessageCreateResponseDto
            {
                Message = "Message sent."
            };
        }

        public async Task<MessageDeleteResponseDto> DeleteAsync(string messageCode, int userId)
        {
            if (string.IsNullOrWhiteSpace(messageCode))
                throw new ArgumentException("Message code can't be empty.");

            var message = await _messageRepo.MessageExistsAsync(messageCode);
            if (message == null)
                throw new ArgumentException("No message found.");

            if (message.UserId != userId)
                throw new ArgumentException("You have not sent a message with that message code.");

            await _messageRepo.DeleteMessageAsync(message);

            return new MessageDeleteResponseDto
            {
                Message = "Message successfully deleted."
            };
        }
    }
}