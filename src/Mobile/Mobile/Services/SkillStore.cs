using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class SkillStore : ISkillStore
    {
        //------------------------------
        //          Fields
        //------------------------------

        //------------------------------
        //          Constructors
        //------------------------------

        public SkillStore()
        {

        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task<List<string>> Search(string queryString)
        {
            using (var dbContext = new AppDbContext())
            {
                queryString = queryString.Trim().ToLower();
                var matching = await dbContext.Skills
                    .Where(s => s.Name.Contains(queryString))
                    .Select(s => s.Name)
                    .ToListAsync();

                return matching;
            }
        }
    }
}
