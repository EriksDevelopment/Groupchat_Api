using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Core.Security;
using Groupchat_Api.Data.Dtos.User;
using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly JwtService _jwt;
        public UserService(IUserRepo userRepo, JwtService jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Username or password can't be empty.");

            var user = await _userRepo.GetUserNameAsync(dto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new InvalidOperationException("Invalid username or password.");

            var (token, expiresAt) = _jwt.GenerateToken(user.Id, "User");

            return new LoginResponseDto
            {
                AccessKey = token,
                ExpiresAt = expiresAt
            };
        }
    }
}