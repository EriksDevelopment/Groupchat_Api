namespace Groupchat_Api.Data.Dtos.Message
{
    public class MessageCreateRequestDto
    {
        public string InviteCode { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}