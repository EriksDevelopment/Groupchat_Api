namespace Groupchat_Api.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string InviteCode { get; set; } = Guid.NewGuid().ToString("N");
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}