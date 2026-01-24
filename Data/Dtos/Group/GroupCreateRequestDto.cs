namespace Groupchat_Api.Data.Dtos.Group
{
    public class GroupCreateRequestDto
    {
        public string GroupName { get; set; } = null!;
        public string? Bio { get; set; }
    }
}