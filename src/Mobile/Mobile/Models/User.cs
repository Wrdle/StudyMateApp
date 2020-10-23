﻿using System.Collections.Generic;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public ImageSource ProfilePicture { get; set; }
        public List<string> Skills { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Group> Groups { get; set; }

        public User()
        {
            Skills = new List<string>();
            Assignments = new List<Assignment>();
            Groups = new List<Group>();
        }

    }
}
