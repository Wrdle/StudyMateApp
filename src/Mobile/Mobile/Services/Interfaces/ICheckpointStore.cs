using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICheckpointStore
    {
        // Commands
        Task<Checkpoint> Add(Checkpoint checkpoint);
        Task UpdateNotes(long checkpointId, string text);
        Task AssignToUser(long checkpointId, long userId);
        Task Remove(long checkpointId);
        Task UnassignUser(long checkpointId, long userId);
        Task<ChecklistItem> AddTaskToCheckpoint(long checkpointId, string task);
        Task<ChecklistItem> UpdateTaskFromCheckpoint(long checkpointId, ChecklistItem task);
        Task RemoveTaskFromCheckpoint(long checkpointId, long taskId);

        // Queries
        Task<ICollection<Checkpoint>> GetByAssignmentId(long assignmentId);

        Task<Checkpoint> GetById(long id);

        /// <summary>
        /// Get all checkpoints associated with a user
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <returns>List of checkpoints</returns>
        Task<ICollection<Checkpoint>> GetByUserId(long userId);
    }
}