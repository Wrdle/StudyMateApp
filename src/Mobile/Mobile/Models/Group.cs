using System.Collections.Generic;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public int? CoverColorId { get; set; }
        public ICollection<UserListItem> Members { get; private set; }
        public ICollection<AssignmentListItem> Assignments { get; private set; }

        public Group()
        {
            Members = new List<UserListItem>();
            Assignments = new List<AssignmentListItem>();
        }

    }
}