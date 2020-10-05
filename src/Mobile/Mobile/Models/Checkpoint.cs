﻿using System;
using System.Collections.Generic;

namespace Mobile.Models
{
    public class Checkpoint
    {
        public long Id { get; set; }

        public long AssignmentId { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime DueDate { get; set; }

        public List<UserListItem> AssignedUsers { get; set; }

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

        }
    }
}