using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Referencing Entities
        public ICollection<UserSkill> UserSkills { get; set; }

        public Skill()
        {
            UserSkills = new List<UserSkill>();
        }
    }
}
