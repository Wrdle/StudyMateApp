using Mobile.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICheckpointStore<T>
    {
        // Get the data from data store
        //Queries
        Task<ICollection<T>> GetAllCheckpointsAsync();

        Task<ICollection<T>> GetAllCheckpointsByAssignmentIDAsync(long assignmentID);

        Task<Checkpoint> GetCheckpointByID(long checkpointID);
    }
}
