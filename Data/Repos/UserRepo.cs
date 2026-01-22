using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Groupchat_Api.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly GroupchatDbContext _context;

        public UserRepo(GroupchatDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserNameExistsAsync(string userName) =>
            await _context.Users.AnyAsync(u => u.UserName == userName);

        public async Task<User?> GetUserNameAsync(string userName) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        public async Task<User> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetIdAsync(int id) =>
           await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}