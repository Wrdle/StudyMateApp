using Mobile.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mobile.Services.Interfaces
{
    public interface ISkillsStore<T>
    {
        // Commands

        // Queries
        Task<ICollection<T>> GetAllSkillsByUserAsync(int userID, bool forceRefresh = false);
    }
}
