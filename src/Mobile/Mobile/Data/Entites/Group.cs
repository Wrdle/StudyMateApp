using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] CoverPhoto { get; set; }
        public string CoverColour { get; set; }

        // Referencing Entities
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<GroupAssignment> GroupAssignments { get; set; }

        public Group()
        {
            UserGroups = new List<UserGroup>();
            GroupAssignments = new List<GroupAssignment>();
        }

    }
}
