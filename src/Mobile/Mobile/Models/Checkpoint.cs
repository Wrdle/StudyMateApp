using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mobile.Models
{
    public class Checkpoint
    {
        public long Id { get; set; }

        public long AssignmentId { get; set; }
        
        public string Title { get; set; }
        
        public string Notes { get; set; }

        public DateTime DueDate { get; set; }

        public List<CheckpointUserListItem> AssignedUsers { get; set; }

        public List<ChecklistItem> ChecklistItems { get; set; }

        public bool IsDone
        {
            get
            {
                for (int i = 0; i < ChecklistItems.Count; i++)
                {
                    if (!ChecklistItems[i].IsDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Returns DueDate as a formatted string
        /// </summary>
        public string DueDateString
        {
            get
            {
                return ("Due " + DueDate.ToString("ddd d \\o\\f MMMM yyyy")).ToUpper();
            }
        }

        public Checkpoint()
        {
            new List<CheckpointUserListItem>();
        }
    }
}
