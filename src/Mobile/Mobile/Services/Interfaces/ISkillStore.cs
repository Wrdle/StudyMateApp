using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ISkillStore
    {
        // Commands
        Task Add(string skill);
        Task<List<Skill>> AddRange(List<Skill> skills);

        // Queries
        Task<List<Skill>> Search(string queryString);
    }
}