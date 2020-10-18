using System.Collections.Generic;

namespace Mobile.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverColour { get; set; }
        public ICollection<UserListItem> Members { get; private set; }
        public ICollection<AssignmentListItem> Assignments { get; private set; }

        public Group()
        {
            Members = new List<UserListItem>();
            Assignments = new List<AssignmentListItem>();
        }

    }
}
