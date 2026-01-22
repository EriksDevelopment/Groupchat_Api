using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User> AddUserAsync(User user);
        Task<bool> UserNameExistsAsync(string userName);
        Task<User?> GetUserNameAsync(string userName);
        Task<User> DeleteUserAsync(User user);
        Task<User?> GetIdAsync(int id);
    }
}