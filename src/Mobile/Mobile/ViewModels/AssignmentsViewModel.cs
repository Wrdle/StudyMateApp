using Mobile.Helpers;
using Mobile.Models;
using Mobile.ViewModels.Assignments;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AssignmentsViewModel : Mobile.ViewModels.BaseViewModel
    {
        private Assignment _selectedAssignment;

        public ObservableCollection<Assignment> Assignments { get; }
        public Command LoadAssignmentsCommand { get; }
        
        public Command AddAssignmentCommand { get; }
        public Command<Assignment> AssignmentTapped { get; }

        public AssignmentsViewModel()
        {
            Title = "Assignments";
            Assignments = new ObservableCollection<Assignment>();
            LoadAssignmentsCommand = new Command(async () => await ExecuteLoadAssignmentsCommand());

            AssignmentTapped = new Command<Assignment>(OnAssignmentSelected);

            AddAssignmentCommand = new Command(OnAddAssignmentTapped);
        }

        /// <summary>
        /// Load Assignments from datastore
        /// </summary>
        /// <returns>Task type</returns>
        async Task ExecuteLoadAssignmentsCommand()
        {
            IsBusy = true;

            try
            {
                Assignments.Clear();
                var assignments = await AssignmentStore.GetByUserIdAsync(1, true);
                foreach (var assignment in assignments)
                {
                    if (assignment.CoverPhoto != null)
                    {
                        assignment.CoverColour = SMColours.DarkGray;
                    }
                    Assignments.Add(assignment);
                }
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

        /// <summary>
        /// Runs before page appears, resetting variables and sets IsBusy to true
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedAssignment = null;
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
    }
}