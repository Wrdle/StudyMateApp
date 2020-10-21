using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ISkillStore
    {
        // Commands
        Task Add(string skill);

        // Queries
        Task<List<string>> Search(string queryString);
    }
}