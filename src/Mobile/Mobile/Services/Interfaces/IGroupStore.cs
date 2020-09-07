using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IGroupStore
    {
        // Commands
        Task Create(string name);

        // Queries
        Task<ICollection<GroupListItem>> MyGroups();
        Task<Group> GetById(long id);

    }
}
