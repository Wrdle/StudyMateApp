using Mobile.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mobile.Services.Interfaces
{
    public interface ISkillsStore<T>
    {
        Task<ICollection<T>> GetAllSkillsByUserIDAsync(int userID, bool forceRefresh = false);
    }
}
