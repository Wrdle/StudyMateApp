using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(AssignmentID), nameof(AssignmentID))]
    class AssignmentViewModel : BaseViewModel
    {
        private string assignmentID;
        private string dueDate;
        private string description;
        private bool showCoverPhoto;
        private Color coverBackgroundColour;
        private ImageSource coverPhoto;
        private Assignment assignment;

        private ObservableCollection<Checkpoint> checkpoints;

        private Checkpoint _selectedCheckpoint;

        public Command<Checkpoint> CheckpointTapped { get; }

        /// <summary>
        /// Constructor for AssignmentViewModel. Called on class instantiation
        /// </summary>
        public AssignmentViewModel()
        {
            Title = "Assignment";
            Debug.WriteLine("We made it to the assignment page");

            CheckpointTapped = new Command<Checkpoint>(OnCheckpointSelected);
        }

        public Assignment Assignment
        {
            get => assignment;
        }

        public string AssignmentID
        {
            get => assignmentID;
            set
            {
                SetProperty(ref assignmentID, value);
                LoadAssignmentId(value);
            }
        }

        public string DueDate
        {
            get => "Final Due: " + dueDate;
            set => SetProperty(ref dueDate, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public bool ShowCoverPhoto
        {
            get => showCoverPhoto;
            set => SetProperty(ref showCoverPhoto, value);
        }

        public Color CoverBackgroundColour
        {
            get => coverBackgroundColour;
            set => SetProperty(ref coverBackgroundColour, value);
        }

        public ImageSource CoverPhoto
        {
            get
            {
                return coverPhoto;
            }
            set
            {
                SetProperty(ref coverPhoto, value);
            }
        }

        public ObservableCollection<Checkpoint> Checkpoints
        {
            get => checkpoints;
            set
            {
                SetProperty(ref checkpoints, value);
            }
        }

        public Checkpoint SelectedCheckpoint
        {
            get => _selectedCheckpoint;
            set
            {
                SetProperty(ref _selectedCheckpoint, value);
                OnCheckpointSelected(value);
            }
        }

        /// <summary>
        /// Navigate to selected checkpoint page
        /// </summary>
        /// <param name="checkpoint">Checkpoint to navigate to</param>
        async void OnCheckpointSelected(Checkpoint checkpoint)
        {
            if (checkpoint == null)
                return;

            // This will push the CheckpointPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignmentCheckpoint?{nameof(CheckpointViewModel.CheckpointID)}={checkpoint.Id}");
        }

        /// <summary>
        /// Loads a assignment from the datastore given an assignment id
        /// </summary>
        /// <param name="id">Assignment Id</param>
        public async void LoadAssignmentId(string id)
        {
            try
            {
                OnPropertyChanged(nameof(Checkpoints));

                long assignmentID = Convert.ToInt64(id);

                assignment = await AssignmentStore.GetById(assignmentID);
                Title = assignment.Title;
                DueDate = assignment.DateDue.ToShortDateString();
                Description = assignment.Description;
                ShowCoverPhoto = CheckCoverPhoto();
                CoverBackgroundColour = assignment.CoverColor.BackgroundColor;

                LoadCheckpoints(assignmentID);

                Debug.WriteLine("Data loaded successfully.");
                //OnPropertyChanged(nameof(Description));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Loads checkpoints from datastore by assignment
        /// </summary>
        /// <param name="id">id of assignment</param>
        public async void LoadCheckpoints(long id)
        {
            var requestedCheckpoints = await CheckpointStore.GetByAssignmentId(id);

            Checkpoints = new ObservableCollection<Checkpoint>();

            foreach (Checkpoint checkpoint in requestedCheckpoints)
            {
                Checkpoints.Add(checkpoint);
            }
        }

        /// <summary>
        /// Check if assignment has a coverphoto
        /// </summary>
        /// <returns>True/False</returns>
        public bool CheckCoverPhoto()
        {
            if (assignment.CoverPhoto != null)
            {
                CoverPhoto = assignment.CoverPhoto;
                return true;
            }
            return false;
        }
    }
}
