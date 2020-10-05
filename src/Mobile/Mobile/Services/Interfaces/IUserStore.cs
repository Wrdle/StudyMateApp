using Mobile.Models;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IUserStore<T>
    {
        long CurrentUserId { get; }
        bool IsLoggedIn { get; }
        Task Login(string email, string password);
        Task Logout();
        Task CreateAccount(string email, string firstName, string lastName);

        Task<User> GetProfile();
        Task<User> UpdateUser(User user);
    }
}
