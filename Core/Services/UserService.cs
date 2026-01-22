using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Data.Dtos.User;
using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Username or password can't be empty.");

            if (await _userRepo.UserNameExistsAsync(dto.UserName))
                throw new ArgumentException("Username already taken.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = passwordHash,
                Bio = dto.Bio
            };

            await _userRepo.AddUserAsync(user);

            return new RegisterResponseDto
            {
                Message = $"Welcome to Groupchat {user.UserName}"
            };
        }
    }
}