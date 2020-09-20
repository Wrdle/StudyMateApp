using System;
using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class Assignment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public byte[] CoverPhoto { get; set; }
        public string CoverColour { get; set; }

        // Referencing Entities
        public ICollection<UserAssignment> UserAssignments { get; set; }
        public ICollection<GroupAssignment> GroupAssignments { get; set; }

        public Assignment()
        {
            UserAssignments = new List<UserAssignment>();
            GroupAssignments = new List<GroupAssignment>();
        }

    }
}
