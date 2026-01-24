
using Groupchat_Api.Data.Dtos.Group;

namespace Groupchat_Api.Core.Interfaces
{
    public interface IGroupService
    {
        Task<GroupCreateResponseDto> CreateAsync(GroupCreateRequestDto dto, int creatorUserId);
        Task<List<GroupShowResponseDto>> ShowAsync(int userId);
        Task<GroupJoinResponseDto> JoinAsync(GroupJoinRequestDto dto, int userId);
        Task<GroupLeaveResponseDto> LeaveAsync(string inviteCode, int userId);
    }
}