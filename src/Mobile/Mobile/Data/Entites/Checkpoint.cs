using System;
using System.Collections.Generic;

namespace Mobile.Data.Entites
{
    public class Checkpoint
    {
        public long Id { get; set; }
        public long AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateDue { get; set; }

        // Referenced Entities
        public Assignment Assignment { get; set; }

        // Referencing Entities
        public ICollection<UserCheckpoint> UserCheckpoints { get; set; }
        public ICollection<ChecklistItem> ChecklistItems { get; set; }

        public Checkpoint()
        {
            UserCheckpoints = new List<UserCheckpoint>();
            ChecklistItems = new List<ChecklistItem>();
        }

    }
}