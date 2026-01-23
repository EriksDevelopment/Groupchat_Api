namespace Groupchat_Api.Data.Dtos
{
    public class ShowResponseDto
    {
        public string Creator { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string? Bio { get; set; }
        public int Members { get; set; }
        public string InviteCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}