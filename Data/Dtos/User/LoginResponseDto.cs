namespace Groupchat_Api.Data.Dtos.User
{
    public class LoginResponseDto
    {
        public string AccessKey { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}