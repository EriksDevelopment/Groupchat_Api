using System.Security.Claims;
using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos;
using Groupchat_Api.Data.Dtos.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Groupchat_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;
        public GroupController(IGroupService groupService, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult<GroupCreateResponseDto>> Create(GroupCreateRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _groupService.CreateAsync(dto, user);

                _logger.LogInformation("Group successfully created.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while creating group.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("show")]
        public async Task<ActionResult<List<GroupShowResponseDto>>> Show()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _groupService.ShowAsync(userId);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving all groups.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("join")]
        public async Task<ActionResult<GroupJoinResponseDto>> Join(GroupJoinRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _groupService.JoinAsync(dto, user);

                _logger.LogInformation("Successfully joined group.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while joining group.");
                return StatusCode(500, "Something went wrong.");
            }
        }


    }
}