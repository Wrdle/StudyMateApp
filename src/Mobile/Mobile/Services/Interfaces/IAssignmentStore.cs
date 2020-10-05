using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IAssignmentStore
    {
        // Commands
        Task<long> Create(Assignment assignment, long? groupId = null);
        Task Update(Assignment assignment);
        Task Delete(long id);

        // Queries
        Task<ICollection<Assignment>> GetByGroupId(long groupId);
        Task<Assignment> GetById(long id);
        Task<ICollection<Assignment>> GetByUserIdAsync(long userId, bool includeGroupAssignments);
    }
}
