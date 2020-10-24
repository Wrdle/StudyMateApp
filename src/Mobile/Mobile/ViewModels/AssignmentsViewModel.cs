using Mobile.Helpers;
using Mobile.ViewModels.Assignments;
using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

// Helpers
using static Mobile.Helpers.Helpers;
using System.Collections.Generic;

namespace Mobile.ViewModels
{
    public class AssignmentsViewModel : Mobile.ViewModels.BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        private Assignment _selectedAssignment;
        private bool showArchived = false;

        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<Assignment> AssignmentsWithArchived { get; set; }
        public ObservableCollection<Assignment> AssignmentsWithoutArchived { get; set; }

        public Command LoadAssignmentsCommand { get; }
        public Command ShowArchivedCommand { get; }
        public Command AddAssignmentCommand { get; }
        public Command<Assignment> AssignmentTapped { get; }

        public string AssignmentsHeading
        {
            get
            {
                if (showArchived == false)
                    return "Current Assignments";
                return "Archived Assignments";
            }
        }

        public string ShowArchivedButtonText
        {
            get
            {
                if (showArchived == false)
                    return "Show Archived";
                return "Show Current";
            }
        }

        public Assignment SelectedAssignment
        {
            get => _selectedAssignment;
            set
            {
                SetProperty(ref _selectedAssignment, value);
                OnAssignmentSelected(value);
            }
        }

        //------------------------------
        //          Constructors
        //------------------------------

        public AssignmentsViewModel()
        {
            Title = "Assignments";

            Assignments = new ObservableCollection<Assignment>();
            AssignmentsWithArchived = new ObservableCollection<Assignment>();
            AssignmentsWithoutArchived = new ObservableCollection<Assignment>();

            LoadAssignmentsCommand = new Command(async () => await ExecuteLoadAssignmentsCommand());
            ShowArchivedCommand = new Command(OnShowArchivedSelected);
            AssignmentTapped = new Command<Assignment>(OnAssignmentSelected);
            AddAssignmentCommand = new Command(OnAddAssignmentTapped);

            OnPropertyChanged(nameof(ShowArchivedButtonText));
        }

        //------------------------------
        //          Methods
        //------------------------------

        /// <summary>
        /// Runs before page appears, resetting variables and sets IsBusy to true
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedAssignment = null;
        }

        /// <summary>
        /// Load Assignments from datastore
        /// </summary>
        /// <returns>Task type</returns>
        async Task  ExecuteLoadAssignmentsCommand()
        {
            IsBusy = true;

            try
            {
                // Start clear
                Assignments.Clear();
                AssignmentsWithoutArchived.Clear();
                AssignmentsWithArchived.Clear();

                // Get data from db
                var assignmentsWithoutArchived = await AssignmentStore.GetByUserIdAsync(userId: LoggedInUser.Id, includeGroupAssignments: true);
                var assignmentsWithArchived = await AssignmentStore.GetByUserIdAsync(userId: LoggedInUser.Id, includeGroupAssignments: true, includeArchived: true);

                // Assign without archived data
                AssignmentsWithoutArchived = ConvertListToObservableCollection(assignmentsWithoutArchived.ToList());
                Assignments = AssignmentsWithoutArchived;

                AssignmentsWithArchived = ConvertListToObservableCollection<Assignment>(RemoveNotArchived(assignmentsWithArchived.ToList()));

                OnPropertyChanged(nameof(Assignments));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnShowArchivedSelected()
        {
            if (showArchived == false)
            {
                Assignments = AssignmentsWithArchived;
                showArchived = true;
            }
            else
            {
                Assignments = AssignmentsWithoutArchived;
                showArchived = false;
            }

            // Update variables on switch
            OnPropertyChanged(nameof(Assignments));
            OnPropertyChanged(nameof(AssignmentsHeading));
            OnPropertyChanged(nameof(ShowArchivedButtonText));
        }

        /// <summary>
        /// Runs when assignment selected, navigating to assignment page of given assignment
        /// </summary>
        /// <param name="assignment">Assignment to navigate to</param>
        async void OnAssignmentSelected(Assignment assignment)
        {
            if (assignment == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignment?{nameof(AssignmentViewModel.AssignmentID)}={assignment.Id}");
        }

        async void OnAddAssignmentTapped()
        {
            await Shell.Current.GoToAsync($"assignments/addAssignment");
        }

        public List<Assignment> RemoveNotArchived(List<Assignment> assignments)
        {
            List<Assignment> filtered = new List<Assignment>();

            foreach (Assignment a in assignments)
            {
                if (a.IsArchived == true)
                {
                    filtered.Add(a);
                }
            }

            return filtered;
        }
    }
}