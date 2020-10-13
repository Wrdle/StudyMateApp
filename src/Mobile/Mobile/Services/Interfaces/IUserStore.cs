using Mobile.Models;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IUserStore
    {
        long CurrentUserId { get; }
        bool IsLoggedIn { get; }
        Task Login(string email, string password);
        void Logout();
        Task CreateAccount(string email, string firstName, string lastName);

        Task<User> GetProfile();
        Task<User> UpdateUser(User user);
    }
}
