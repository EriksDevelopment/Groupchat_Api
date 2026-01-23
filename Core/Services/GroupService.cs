using Groupchat_Api.Data.Models;
using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos.Group;
using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Dtos;
using Azure;

namespace Groupchat_Api.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepo _groupRepo;
        private readonly IUserRepo _userRepo;
        public GroupService(IGroupRepo groupRepo, IUserRepo userRepo)
        {
            _groupRepo = groupRepo;
            _userRepo = userRepo;
        }

        public async Task<CreateResponseDto> CreateAsync(CreateRequestDto dto, int creatorUserId)
        {
            if (string.IsNullOrWhiteSpace(dto.GroupName))
                throw new ArgumentException("Group name can't be empty.");

            var group = new Group
            {
                GroupName = dto.GroupName,
                Bio = dto.Bio
            };

            var createdGroup = await _groupRepo.AddGroupAsync(group, creatorUserId);

            return new CreateResponseDto
            {
                Message = "Group created successfully!",
                GroupName = createdGroup.GroupName,
                Bio = createdGroup.Bio,
                InviteCode = createdGroup.InviteCode,
                CreatedAt = createdGroup.CreatedAt
            };
        }

        public async Task<List<ShowResponseDto>> ShowAsync(int userId)
        {
            var groups = await _groupRepo.GetGroupAsync(userId);

            if (!groups.Any())
                throw new ArgumentException("You have not joined any group.");

            return groups.Select(group => new ShowResponseDto
            {
                Creator = group.GroupUsers
                    .FirstOrDefault(gu => gu.IsAdmin)?
                    .User.UserName ?? "Unknown",
                GroupName = group.GroupName,
                Bio = group.Bio,
                Members = group.GroupUsers.Count,
                InviteCode = group.InviteCode,
                CreatedAt = group.CreatedAt
            }).ToList();
        }

        public async Task<JoinResponseDto> JoinAsync(JoinRequestDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.InviteCode))
                throw new ArgumentException("Invite code can't be empty.");

            var group = await _groupRepo.GetInviteCodeAsync(dto.InviteCode);
            if (group == null)
                throw new ArgumentException("No group found.");

            var user = await _userRepo.GetIdAsync(userId);

            var alreadyMember = group.GroupUsers.Any(gu => gu.UserId == userId);
            if (alreadyMember)
                throw new ArgumentException("Already a member of this group.");

            var groupUser = new GroupUser
            {
                GroupId = group.Id,
                UserId = userId,
                IsAdmin = false,
                JoinedAt = DateTime.UtcNow
            };

            await _groupRepo.AddGroupUserAsync(groupUser);

            return new JoinResponseDto
            {
                Message = $"You have successfully joined group '{group.GroupName}'"
            };
        }
    }
}