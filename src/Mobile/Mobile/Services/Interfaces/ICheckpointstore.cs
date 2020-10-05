using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICheckpointstore
    {
        // Commands
        Task<Checkpoint> Add(Checkpoint checkpoint);
        Task Remove(long checkpointId);
        Task AssignToUser(long checkpointId, long userId);
        Task UnassignUser(long checkpointId, long userId);

        // Queries
        Task<ICollection<Checkpoint>> GetByAssignmentId(long assignmentId);
    }
}
