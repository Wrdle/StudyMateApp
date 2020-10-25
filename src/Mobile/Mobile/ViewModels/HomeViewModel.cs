using Mobile.Models;
using Mobile.ViewModels.Assignments;
using Mobile.ViewModels.Groups;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

// Helpers
using static Mobile.Helpers.Helpers;

namespace Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Commands
        public Command<Assignment> AssignmentTapped { get; }
        public Command<Checkpoint> CheckpointTapped { get; }
        public Command<GroupListItem> GroupTapped { get; }

        // Collections
        public ObservableCollection<Checkpoint> Checkpoints7Days { get; set; }
        public ObservableCollection<GroupListItem> Groups { get; set; }

        // Other
        private Assignment _assignment;
        public Assignment Assignment
        {
            get => _assignment;
            set => SetProperty(ref _assignment, value);
        }

        //------------------------------
        //          Constructors
        //------------------------------

        public HomeViewModel()
        {
            Title = "Home";

            AssignmentTapped = new Command<Assignment>(OnAssignmentSelected);
            CheckpointTapped = new Command<Checkpoint>(OnCheckpointSelected);
            GroupTapped = new Command<GroupListItem>(OnGroupSelected);
        }

        //------------------------------
        //          Methods
        //------------------------------

        public void OnAppearing()
        {
            LoadData();
        }

        async void OnGroupSelected(GroupListItem selected)
        {
            if (selected == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"groups/group?{nameof(GroupViewModel.GroupID)}={selected.Id}");
        }

        async void OnAssignmentSelected(Assignment selected)
        {
            if (selected == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignment?{nameof(AssignmentViewModel.AssignmentID)}={Assignment.Id}");
        }

        async void OnCheckpointSelected(Checkpoint selected)
        {
            if (selected == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignmentCheckpoint?{nameof(CheckpointViewModel.CheckpointID)}={selected.Id}");
        }

        public void LoadData()
        {
            Assignment = LoadNextAssignment();
            Checkpoints7Days = LoadCheckpointsNext7Days();
            Groups = Load4Groups();

            OnPropertyChanged(nameof(Assignment));
            OnPropertyChanged(nameof(Checkpoints7Days));
            OnPropertyChanged(nameof(Groups));
        }

        public ObservableCollection<Checkpoint> LoadCheckpointsNext7Days()
        {
            var checkpoints = CheckpointStore.GetByUserId(LoggedInUser.Id).Result.Where(c => c.DueDate < DateTime.UtcNow.AddDays(7)).ToList();
            return ConvertListToObservableCollection(checkpoints);
        }

        public Assignment LoadNextAssignment()
        {
            var assignments = AssignmentStore.GetByUserIdAsync(LoggedInUser.Id, true).Result;
            if (assignments.Count > 0)
            {
                assignments = assignments.OrderBy(a => a.DateDue).ToList();
                return assignments.First();
            }
            return null;
        }

       public ObservableCollection<GroupListItem> Load4Groups()
        {
            var groups = GroupStore.MyGroups().Result.ToList();

            // Ensure no more than 4 groups is returned
            if (groups.Count > 4)
            {
                groups = groups.GetRange(0, 4);
            }

           return ConvertListToObservableCollection(groups);
        }
    }
}
