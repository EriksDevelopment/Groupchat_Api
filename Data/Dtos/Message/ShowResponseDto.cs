namespace Groupchat_Api.Data.Dtos.Message
{
    public class ShowResponseDto
    {
        public string UserName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}