using Mobile.Models;
using Mobile.ViewModels.Assignments;
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

        // Collections
        public ObservableCollection<Checkpoint> Checkpoints7Days;
        public ObservableCollection<GroupListItem> Groups;

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

            LoadData();
        }

        //------------------------------
        //          Methods
        //------------------------------

        async void OnAssignmentSelected(Assignment selected)
        {
            if (selected == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignment?{nameof(AssignmentViewModel.AssignmentID)}={Assignment.Id}");
        }

        public void LoadData()
        {
            Assignment = LoadNextAssignment();
            Checkpoints7Days = LoadCheckpointsNext7Days();
            Groups = Load4Groups();

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

            // Ensure no more than 4 checkpoints is returned
            /*if (groups.Count > 4)
            {
                groups = groups.GetRange(0, 4);
            }*/

           return ConvertListToObservableCollection(groups);
        }
    }
}
