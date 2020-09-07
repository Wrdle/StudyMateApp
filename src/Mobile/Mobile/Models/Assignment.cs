using System;
using System.Collections.Generic;

namespace Mobile.Models
{
    public class Assignment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public DateTime DateDue { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverColour { get; set; }

        public Assignment()
        {
            Skills = new List<Skill>();
        }

    }
}
