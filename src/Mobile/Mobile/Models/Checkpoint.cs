using Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

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

        // Checklist tasks
        public string Filename { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Returns DueDate as a formatted string
        /// </summary>
        public string DueDateString
        {
            get
            {
                return ("Due: " + DueDate.ToString("ddd d \\o\\f MMMM yyyy"));
            }
        }

        public string HomePageString
        {
            get
            {
                IAssignmentStore AssignmentStore = DependencyService.Get<IAssignmentStore>();
                var assignment = AssignmentStore.GetById(AssignmentId).Result;

                var assignmentTitle = assignment.Title;
                var groupName = assignment.GroupName;

                var day = Date.DayOfWeek.ToString();

                if (groupName != null)
                {
                    return assignmentTitle + " - " + groupName + " - " + day;
                }
                return assignmentTitle + " - " + day;
            }
        }

        public Checkpoint()
        {
            new List<CheckpointUserListItem>();
        }
    }
}
