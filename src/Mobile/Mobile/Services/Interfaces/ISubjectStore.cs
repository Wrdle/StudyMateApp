using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Mobile.Services.Interfaces
{
    public interface ISubjectStore<T>
    {
        // Commands

        // Queries

        Task<ICollection<T>> GetAllSubjectsByUserAsync(int userID, bool forceRefresh = false);
    }
}
