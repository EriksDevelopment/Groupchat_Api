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
        public GroupService(IGroupRepo groupRepo)
        {
            _groupRepo = groupRepo;
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

        public async Task<List<ShowResponseDto>> ShowAsync()
        {
            var groups = await _groupRepo.GetGroupAsync();

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
    }
}