using System;

namespace Mobile.Models
{
    public class ChecklistItem
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
