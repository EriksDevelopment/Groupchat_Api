namespace Groupchat_Api.Data.Models
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsAdmin { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}