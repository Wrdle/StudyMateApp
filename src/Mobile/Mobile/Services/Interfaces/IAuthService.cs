using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Fetch JWT for user after authenticating.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(string email, string password);

        Task CreateAccount(string email, string firstName, string lastName);
    }
}
