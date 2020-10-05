using Mobile.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICheckpointStore<T>
    {

        //Queries
        Task<ICollection<T>> GetAllCheckpointsAsync();

        Task<ICollection<T>> GetAllCheckpointsByAssignmentIDAsync(long assignmentID);
    }
}
