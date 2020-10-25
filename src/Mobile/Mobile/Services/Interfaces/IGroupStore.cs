using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface IGroupStore
    {
        // Commands
        Task Create(string name);
        Task Update(Group group);
        Task Leave(long id);

        // Queries
        Task<ICollection<GroupListItem>> Search(string searchTerm);
        Task<ICollection<GroupListItem>> MyGroups();
        Task<Group> GetById(long id);

    }
}