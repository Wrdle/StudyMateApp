using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class Assignment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public DateTime DateDue { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public CoverColor CoverColor { get; set; }

        public Assignment()
        {
            Skills = new List<Skill>();
        }
    }
}
