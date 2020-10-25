using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels.Groups
{
    [QueryProperty(nameof(GroupIdParam), nameof(GroupId))]
    public class GroupInfoViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Query Params
        public string GroupIdParam { set => GroupId = Convert.ToInt64(value); }

        // Private
        private long _groupId;
        private Group _group;
        private bool _hasCoverPhoto;
        private ObservableCollection<UserListItem> _groupMembers;

        // Accessors
        public long GroupId { get => _groupId; set => SetProperty(ref _groupId, value); }
        public Group Group { get => _group; set => SetProperty(ref _group, value); }
        public bool HasCoverPhoto { get => _hasCoverPhoto; set => SetProperty(ref _hasCoverPhoto, value); }
        public ObservableCollection<UserListItem> GroupMembers { get => _groupMembers; set => SetProperty(ref _groupMembers, value); }

        // Commands
        public ICommand LoadGroupCommand { get; }

        //------------------------------
        //          Constructors
        //------------------------------

        public GroupInfoViewModel()
        {
            LoadGroupCommand = new Command(LoadGroup);
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async void LoadGroup()
        {
            Group = await GroupStore.GetById(GroupId);
            Title = $"{Group.Name} Details";
            HasCoverPhoto = Group.CoverPhoto != null;

            var groupMembers = new ObservableCollection<UserListItem>();
            foreach (var user in Group.Members)
            {
                groupMembers.Add(user);
            }

            GroupMembers = groupMembers;
        }

    }
}
