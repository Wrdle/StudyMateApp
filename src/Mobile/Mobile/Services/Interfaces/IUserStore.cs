using Mobile.Models;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IUserStore
    {
        Task<User> GetProfile();
    }
}
