using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ISkillStore
    {
        // Queries
        Task<List<string>> Search(string queryString);
    }
}