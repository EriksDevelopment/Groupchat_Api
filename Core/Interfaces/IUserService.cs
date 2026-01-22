using Groupchat_Api.Data.Dtos.User;

namespace Groupchat_Api.Core.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task<DeleteResponseDto> DeleteAsync(DeleteRequestDto dto, string userName);
    }
}