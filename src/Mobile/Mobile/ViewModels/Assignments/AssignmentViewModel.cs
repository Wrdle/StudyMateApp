using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(inputAssignmentID), nameof(AssignmentID))]
    class AssignmentViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        private Assignment assignment;
        private long assignmentID;
        private string assignmentNotes;
        private bool showCoverPhoto;
        private Checkpoint _selectedCheckpoint;
        private ObservableCollection<Checkpoint> checkpoints;

        public Command<Checkpoint> CheckpointTapped { get; }

        // Current Assignment
        public Assignment Assignment
        {
            get => assignment;
            set => SetProperty(ref assignment, value);
        }

        // ASSIGNMENT ID
        public string inputAssignmentID
        {
            set => AssignmentID = Convert.ToInt64(value);
        }

        public long AssignmentID
        {
            get => assignmentID;
            set
            {
                SetProperty(ref assignmentID, Convert.ToInt64(value));
                LoadAssignmentId(value);
            }
        }

        // Assignment Notes
        public string AssignmentNotes
        {
            get => assignmentNotes;
            set
            {
                // Update Locally
                SetProperty(ref assignmentNotes, value);

                if (value != null)
                {
                    assignment.Notes = value;

                    // Update DB 
                    AssignmentStore.Update(assignment);
                }
            }
        }

        // SHOW COVER PHOTO
        public bool ShowCoverPhoto
        {
            get => showCoverPhoto;
            set => SetProperty(ref showCoverPhoto, value);
        }

        // CHECKPOINTS
        public ObservableCollection<Checkpoint> Checkpoints
        {
            get => checkpoints;
            set => SetProperty(ref checkpoints, value);
        }

        // Checkpoint Tapped
        public Checkpoint SelectedCheckpoint
        {
            get => _selectedCheckpoint;
            set
            {
                SetProperty(ref _selectedCheckpoint, value);
                OnCheckpointSelected(value);
            }
        }

        //------------------------------
        //          Constructors
        //------------------------------

        /// <summary>
        /// Constructor for AssignmentViewModel. Called on class instantiation
        /// </summary>
        public AssignmentViewModel()
        {
            Title = "Assignment";

            CheckpointTapped = new Command<Checkpoint>(OnCheckpointSelected);
        }

        //------------------------------
        //          Methods
        //------------------------------

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
        public async void LoadAssignmentId(long id)
        {
            try
            {
                // Get Assignment
                Assignment = await AssignmentStore.GetById(id);

                // Extract and store data
                Title = assignment.Title;
                AssignmentNotes = assignment.Notes;
                ShowCoverPhoto = CheckCoverPhoto();

                var coverColors = await CoverColorStore.GetAll();

                // Load the assignments checkpoints
                LoadCheckpoints(Assignment.Id);
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
        /// Loads checkpoints from datastore
        /// </summary>
        /// <param name="id">id of checkpoint</param>
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
                return true;
            }
            return false;
        }
    }
}
