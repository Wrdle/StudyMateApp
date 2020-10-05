using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICheckpointStore
    {
        // Commands
        Task<Checkpoint> Add(Checkpoint checkpoint);
        Task AssignToUser(long checkpointId, long userId);
        Task Remove(long checkpointId);
        Task UnassignUser(long checkpointId, long userId);

        // Queries
        Task<ICollection<Checkpoint>> GetByAssignmentId(long assignmentId);
        Task<Checkpoint> GetById(long id);

    }
}
