using Groupchat_Api.Data.Models;

namespace Groupchat_Api.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User> AddUserAsync(User user);
        Task<User?> UserNameExistsAsync(string userName);
    }
}