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
        public string Notes { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public DateTime DateDue { get; set; }
        public bool IsArchived { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public CoverColor CoverColor { get; set; }

        /// <summary>
        /// Returns DueDate as a formatted string
        /// </summary>
        public string DateDueString
        {
            get
            {
                return ("Due " + DateDue.ToString("ddd d \\o\\f MMMM yyyy")).ToUpper();
            }
        }

        public string DateDueSlashNotation
        {
            get
            {
                return "Due: " + DateDue.ToString("d/M/yyyy");
            }
        }

        public Assignment()
        {
            Skills = new List<Skill>();
        }


        /*public Assignment DeepCopy()
        {
            new Assignment
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Notes = Notes,
                Skills = Skills.Cop
            }
        }*/
    }
}
