namespace Groupchat_Api.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public string MessageCode { get; set; } = Guid.NewGuid().ToString("N");

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

    }
}