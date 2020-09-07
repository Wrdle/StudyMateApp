using System.Collections.Generic;

namespace Mobile.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public ICollection<Skill> Skills { get; private set; }
        public ICollection<Assignment> Assignments { get; private set; }
        public ICollection<Group> Groups { get; private set; }

        public User()
        {
            Skills = new List<Skill>();
            Assignments = new List<Assignment>();
            Groups = new List<Group>();
        }

    }
}
