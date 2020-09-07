using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IAssignmentStore
    {
        // Commands

        // Queries
        Task<ICollection<AssignmentListItem>> GetByGroup(long groupId);
        Task<Assignment> GetById(long id);
    }
}
