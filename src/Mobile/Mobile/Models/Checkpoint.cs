﻿using System;
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

        public List<User> AssignedUsers { get; set; }

        public Checkpoint()
        {

        }
    }
}