using System.Security.Claims;
using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Groupchat_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpGet("/show/{inviteCode}")]
        public async Task<ActionResult<List<MessageShowResponseDto>>> Show(string inviteCode)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _messageService.ShowAsync(inviteCode, user);

                _logger.LogInformation("Messages successfully retrieved.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving messages.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("send")]
        public async Task<ActionResult<MessageCreateResponseDto>> Create([FromBody] MessageCreateRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _messageService.CreateAsync(dto, user);

                _logger.LogInformation("Message successfully sent.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while sending message.");
                return StatusCode(500, "Something went wrong.");
            }

        }
    }
}