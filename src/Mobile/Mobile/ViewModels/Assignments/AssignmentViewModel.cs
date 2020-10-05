using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(inputAssignmentID), nameof(AssignmentID))]
    class AssignmentViewModel : BaseViewModel
    {        
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

        // =============================
        //       ASSIGNMENT DATA
        // =============================

        private Assignment assignment;

        // ASSIGNMENT ID
        public string inputAssignmentID
        {
            set => AssignmentID = Convert.ToInt64(value);
        }

        private long assignmentID;

        public long AssignmentID
        {
            get => assignmentID;
            set
            {
                SetProperty(ref assignmentID, Convert.ToInt64(value));
                LoadAssignmentId(value);
            }
        }


        // DUE DATE
        private string dueDate;

        public string DueDate
        { 
            get => "Final Due: " + dueDate;
            set => SetProperty(ref dueDate, value);
        }


        // DESCRIPTION
        private string description;

        public string Description
        {
                get => description;
                set => SetProperty(ref description, value);
        }


        // SHOW COVER PHOTO
        private bool showCoverPhoto;

        public bool ShowCoverPhoto
        {
            get => showCoverPhoto;
            set => SetProperty(ref showCoverPhoto, value);
        }


        // COVER BACKGROUND COLOUR
        private Color coverBackgroundColour;

        public Color CoverBackgroundColour 
        {
            get => coverBackgroundColour;
            set => SetProperty(ref coverBackgroundColour, value);
        }

        

        // COVER PHOTO
        private ImageSource coverPhoto;

        public ImageSource CoverPhoto
        {
            get => coverPhoto;
            set => SetProperty(ref coverPhoto, value);
        }


        // CHECKPOINTS
        private ObservableCollection<Checkpoint> checkpoints;

        public ObservableCollection<Checkpoint> Checkpoints 
        {
            get => checkpoints;
            set => SetProperty(ref checkpoints, value);
        }

        // =============================
        //       TAP CHECKPOINT
        // =============================

        private Checkpoint _selectedCheckpoint;

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
        public async void LoadAssignmentId(long id)
        {
            try
            {
                // Get Assignment
                assignment = await AssignmentDataStore.GetById(id);

                // Extract and store data
                Title = assignment.Title;
                DueDate = assignment.DateDue.ToShortDateString();
                Description = assignment.Description;
                ShowCoverPhoto = CheckCoverPhoto();
                CoverBackgroundColour = assignment.CoverColour.BackgroundColour;

                // Load the assignments checkpoints
                LoadCheckpoints(assignmentID);
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
                CoverPhoto = assignment.CoverPhoto;
                return true;
            }
            return false;
        }
    }
}
