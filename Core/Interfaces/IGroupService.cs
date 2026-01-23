using Groupchat_Api.Data.Dtos;
using Groupchat_Api.Data.Dtos.Group;

namespace Groupchat_Api.Core.Interfaces
{
    public interface IGroupService
    {
        Task<CreateResponseDto> CreateAsync(CreateRequestDto dto, int creatorUserId);
        Task<List<ShowResponseDto>> ShowAsync();
    }
}