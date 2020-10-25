using System;
using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] CoverPhotoBytes { get; set; }
        public int CoverColorId { get; set; }

        // Referenced Entities
        public CoverColor CoverColor { get; set; }

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
