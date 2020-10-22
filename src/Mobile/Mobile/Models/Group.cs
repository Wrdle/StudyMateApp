using System.Collections.Generic;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public CoverColor CoverColor { get; set; }
        public ICollection<UserListItem> Members { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        public Group()
        {
            Members = new List<UserListItem>();
            Assignments = new List<Assignment>();
        }

    }
}