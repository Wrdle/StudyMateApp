using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using System;
using System.Collections.Generic;
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

        public async void LoadAssignmentId(string id)
        {
            try
            {
                assignment = await DataStore.GetById(Convert.ToInt64(id));
                Title = assignment.Title;
                DueDate = assignment.DateDue.ToShortDateString();
                Description = assignment.Description;
                ShowCoverPhoto = CheckCoverPhoto();
                CoverBackgroundColour = assignment.CoverColour.BackgroundColour;

                //OnPropertyChanged(nameof(DueDate));
                //OnPropertyChanged(nameof(Description));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
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
