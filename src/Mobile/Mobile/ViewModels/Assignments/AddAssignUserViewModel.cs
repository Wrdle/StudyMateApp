using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobile.Models;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    public class AddAssignUserViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Private
        private CheckpointViewModel _parentViewModel;
        private ObservableCollection<CheckpointUserListItem> _unassignedUsers;

        // Public
        public ObservableCollection<CheckpointUserListItem> UnassignedUsers
        {
            get => UnassignedUsers;
            set => SetProperty(ref _unassignedUsers, value);
        }

        // Commands
        public ICommand SelectUserCommand { get; }

        //------------------------------
        //          Constructor
        //------------------------------

        public AddAssignUserViewModel(CheckpointViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;

            // Commands
            SelectUserCommand = new Command<CheckpointUserListItem>(ExecuteSelectUserCommand);
        }

        public AddAssignUserViewModel()
        {
        }

        //------------------------------
        //          Methods
        //------------------------------

        private async void ExecuteSelectUserCommand(CheckpointUserListItem user)
        {
            await CheckpointStore.AssignToUser(_parentViewModel.Checkpoint.Id, user.Id);
            _parentViewModel.LoadCheckpointID(_parentViewModel.CheckpointID);
        }

    }
}
