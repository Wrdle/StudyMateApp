using Mobile.Models;
using System.Collections.Generic;

namespace Mobile.ViewModels
{
    class GroupsViewModel : BaseViewModel
    {
        public ICollection<GroupListItem> Groups { get; set; }

        public GroupsViewModel()
        {
            Title = "Groups";
            Groups = new List<GroupListItem> 
            {
                new GroupListItem { Id = 1, Name = "Group1"},
                new GroupListItem { Id = 2, Name = "Group2"},
                new GroupListItem { Id = 3, Name = "Group3"},
                new GroupListItem { Id = 4, Name = "Group4"},
                new GroupListItem { Id = 5, Name = "Group5"},
                new GroupListItem { Id = 6, Name = "Group6"}
            };
        }
    }
}
