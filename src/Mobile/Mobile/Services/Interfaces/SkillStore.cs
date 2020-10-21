using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillEntity = Mobile.Data.Entites.Skill;

namespace Mobile.Services.Interfaces
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

        public async Task Add(string skill)
        {
            using (var dbContext = new AppDbContext())
            {
                var normalizedSkill = skill.ToLower();
                try
                {
                    await dbContext.Skills.AddAsync(new SkillEntity { Name = normalizedSkill });
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                }
            }
        }

        public async Task<List<string>> Search(string queryString)
        {
            using (var dbContext = new AppDbContext())
            {
                var normalizedQueryString = queryString.ToLower();
                var skills = await dbContext.Skills
                    .Where(s => s.Name.Contains(normalizedQueryString))
                    .Select(s => s.Name)
                    .ToListAsync();

                return skills;
            }
        }

    }
}
