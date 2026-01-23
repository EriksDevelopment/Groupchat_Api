namespace Groupchat_Api.Data.Dtos.Group
{
    public class CreateResponseDto
    {
        public string Message { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string InviteCode { get; set; } = Guid.NewGuid().ToString("N");
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}