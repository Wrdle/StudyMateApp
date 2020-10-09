using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Current { get; set; }

        public Subject (int id, string name, bool current)
        {
            Id = id;
            Name = name;
            Current = current;
        }
    }
}
