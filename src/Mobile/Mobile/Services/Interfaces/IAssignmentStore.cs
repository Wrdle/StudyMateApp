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
        /// <summary>
        /// Gets all assignments associated with a user asynchronous.
        /// </summary>
        /// <param name="userId">ID of type long, indicating the ID of user</param>
        /// <param name="includeGroupAssignments">Bool indicating whether group assignments associated with the user should be included</param>
        /// <returns>Collection of assignments the user has.</returns>
        Task<ICollection<Assignment>> GetByUserIdAsync(long userId, bool includeGroupAssignments);
        /// <summary>
        /// Gets all assignments associated with a user asynchronous. Overloaded method.
        /// </summary>
        /// <param name="userId">ID of type long, indicating the ID of user</param>
        /// <param name="includeGroupAssignments">Bool indicating whether group assignments associated with the user should be included</param>
        /// <param name="includeArchived">Indicates whether to include archived assignments.</param>
        /// <returns>Collection of assignments the user has.</returns>
        Task<ICollection<Assignment>> GetByUserIdAsync(long userId, bool includeGroupAssignments, bool includeArchived);
        Task<long> GenerateNewAssignmentID();
    }
}
