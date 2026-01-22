namespace Groupchat_Api.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Bio { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}