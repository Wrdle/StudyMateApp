using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }

        // Referencing Entities
        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<UserAssignment> UserAssignments { get; set; }
        public ICollection<UserCheckpoint> UserCheckpoints { get; set; }

        public User()
        {
            UserSkills = new List<UserSkill>();
            UserGroups = new List<UserGroup>();
            UserAssignments = new List<UserAssignment>();
            UserCheckpoints = new List<UserCheckpoint>();
        }

    }
}
