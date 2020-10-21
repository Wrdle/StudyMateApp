using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Skill>> AddRange(List<Skill> skills)
        {
            using (var dbContext = new AppDbContext())
            {
                var skillNames = skills.Select(s => s.Name.Trim().ToLower()).ToList();

                var existingSkills = await dbContext.Skills
                    .Where(s => skillNames.Contains(s.Name))
                    .ToListAsync();

                foreach (var s in existingSkills)
                {
                    skillNames.Remove(s.Name);
                }

                var skillEntities = skillNames.Select(s => new SkillEntity { Name = s }).ToList();

                await dbContext.Skills.AddRangeAsync(skillEntities);
                await dbContext.SaveChangesAsync();

                skillEntities.Concat(existingSkills);
                return skillEntities.Select(s => new Skill(s.Id, s.Name)).ToList();
            }
        }

        public async Task<List<Skill>> Search(string queryString)
        {
            using (var dbContext = new AppDbContext())
            {
                var normalizedQueryString = queryString.ToLower();
                var skills = await dbContext.Skills
                    .Where(s => s.Name.Contains(normalizedQueryString))
                    .Select(s => new Skill(s.Id, s.Name))
                    .ToListAsync();

                return skills;
            }
        }

    }
}
