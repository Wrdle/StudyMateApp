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

        public AssignmentViewModel()
        {
            Title = "Assignment";
            Debug.WriteLine("We made it to the assignment page");
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

        public ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public async void LoadAssignmentId(string id)
        {
            try
            {
                long assignmentID = Convert.ToInt64(id);

                assignment = await AssignmentDataStore.GetById(assignmentID);
                Title = assignment.Title;
                DueDate = assignment.DateDue.ToShortDateString();
                Description = assignment.Description;
                ShowCoverPhoto = CheckCoverPhoto();
                CoverBackgroundColour = assignment.CoverColour.BackgroundColour;

                LoadCheckpoints(assignmentID);

                Debug.WriteLine("Data loaded successfully.");

                //OnPropertyChanged(nameof(DueDate));
                //OnPropertyChanged(nameof(Description));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void LoadCheckpoints(long id)
        {
            var checkpoints = await CheckpointDataStore.GetAllCheckpointsByAssignmentIDAsync(id);

            Checkpoints = new ObservableCollection<Checkpoint>();

            foreach (Checkpoint checkpoint in checkpoints)
            {
                Checkpoints.Add(checkpoint);
            }
        }

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
