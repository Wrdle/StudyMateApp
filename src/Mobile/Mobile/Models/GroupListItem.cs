using System;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class GroupListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public CoverColor CoverColor { get; set; }

        public string SemesterAndYear
        {
            get
            {
                var semester = DateCreated.Month > 6 ? 2 : 1;
                var year = DateCreated.Year;
                return $"Semester {semester} | {year}";
            }
        }
    }
}
