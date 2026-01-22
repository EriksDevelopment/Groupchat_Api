namespace Groupchat_Api.Data.Dtos.User
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Bio { get; set; }
    }
}