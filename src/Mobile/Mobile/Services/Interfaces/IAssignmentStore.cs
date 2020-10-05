using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IAssignmentStore<T>
    {
        // Commands
        Task<bool> AddAssignmentAsync(T assignment);

        // Queries
        //Task<ICollection<AssignmentListItem>> GetByGroup(long groupId);
        Task<Assignment> GetById(long id);
        Task<ICollection<T>> GetAllByUserAsync(int userID, bool forceRefresh = false);
        Task<long> GenerateNewAssignmentID();
    }
}
