using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

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
        public SMColour CoverColour { get; set; }

        public Color CoverBackgroundColour 
        {
            get
            {
                return CoverColour.BackgroundColour;
            }
        }

        public Color CoverFontColour
        {
            get
            {
                return CoverColour.FontColour;
            }
        }

        public Assignment()
        {
            Skills = new List<Skill>();
        }
    }
}
